using System;
using UnityEngine;
using UnityEngine.UI;
using GreenNacho.AI.NeuralNetworking;

public class SimulationScreen : MonoBehaviour
{
    [SerializeField] Text generationsText;
    [SerializeField] Text bestFitnessText;
    [SerializeField] Text averageFitnessText;
    [SerializeField] Text worstFitnessText;
    [SerializeField] Text timerText;
    [SerializeField] Slider timerSlider;
    [SerializeField] Button pauseButton;
    [SerializeField] Button stopButton;
    [SerializeField] GameObject startConfigurationScreen;

    string generationsString;
    string bestFitnessString;
    string averageFitnessString;
    string worstFitnessString;
    string timerString;
    int lastGeneration = 0;

    void Start()
    {
        timerSlider.onValueChanged.AddListener(OnTimerChange);
        pauseButton.onClick.AddListener(OnPauseButtonClick);
        stopButton.onClick.AddListener(OnStopButtonClick);
    }

    void OnEnable()
    {
        timerString = timerText.text;
        timerText.text = String.Format(timerString, SimulationManager.Instance.IterationsPerUpdate);

        if (String.IsNullOrEmpty(generationsString))
            generationsString = generationsText.text;
        if (String.IsNullOrEmpty(bestFitnessString))
            bestFitnessString = bestFitnessText.text;
        if (String.IsNullOrEmpty(averageFitnessString))
            averageFitnessString = averageFitnessText.text;
        if (String.IsNullOrEmpty(worstFitnessString))
            worstFitnessString = worstFitnessText.text;

        generationsText.text = String.Format(generationsString, 0);
        bestFitnessText.text = String.Format(bestFitnessString, 0);
        averageFitnessText.text = String.Format(averageFitnessString, 0);
        worstFitnessText.text = String.Format(worstFitnessString, 0);
    }

    void OnTimerChange(float value)
    {
        SimulationManager.Instance.IterationsPerUpdate = (int)value;
        timerText.text = string.Format(timerString, SimulationManager.Instance.IterationsPerUpdate);
    }

    void OnPauseButtonClick()
    {
        SimulationManager.Instance.ChangeSimulationPauseState();
    }

    void OnStopButtonClick()
    {
        SimulationManager.Instance.StopSimulation();
        gameObject.SetActive(false);
        startConfigurationScreen.SetActive(true);
        lastGeneration = 0;
    }

    void LateUpdate()
    {
        if (lastGeneration != SimulationManager.Instance.Generation)
        {
            lastGeneration = SimulationManager.Instance.Generation;
            generationsText.text = String.Format(generationsString, SimulationManager.Instance.Generation);
            bestFitnessText.text = String.Format(bestFitnessString, SimulationManager.Instance.BestFitness);
            averageFitnessText.text = String.Format(averageFitnessString, SimulationManager.Instance.AverageFitness);
            worstFitnessText.text = String.Format(worstFitnessString, SimulationManager.Instance.WorstFitness);
        }
    }
}