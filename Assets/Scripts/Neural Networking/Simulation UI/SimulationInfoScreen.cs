using System;
using UnityEngine;
using UnityEngine.UI;
using GreenNacho.AI.NeuralNetworking;
using TMPro;

public class SimulationInfoScreen : MonoBehaviour
{
    [SerializeField] TMP_Text generationsText = default;
    [SerializeField] TMP_Text bestFitnessText = default;
    [SerializeField] TMP_Text averageFitnessText = default;
    [SerializeField] TMP_Text worstFitnessText = default;
    [SerializeField] TMP_Text timeScaleText = default;
    [SerializeField] Slider timeScaleSlider = default;
    [SerializeField] Button pauseButton = default;
    [SerializeField] Button stopButton = default;
    [SerializeField] SimulationSetUpScreen simulationSetUpScreen = default;

    string generationsString;
    string bestFitnessString;
    string averageFitnessString;
    string worstFitnessString;
    string timerString;
    int lastGeneration = 0;

    void Start()
    {
        timeScaleSlider.onValueChanged.AddListener(OnTimerChange);
        pauseButton.onClick.AddListener(OnPauseButtonClick);
        stopButton.onClick.AddListener(OnStopButtonClick);
    }

    void OnEnable()
    {
        timerString = timeScaleText.text;
        timeScaleText.text = String.Format(timerString, SimulationManager.Instance.IterationsPerUpdate);

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
        timeScaleText.text = string.Format(timerString, (int)value);
    }

    void OnPauseButtonClick()
    {
        SimulationManager.Instance.ChangeSimulationPauseState();
    }

    void OnStopButtonClick()
    {
        SimulationManager.Instance.StopSimulation();
        gameObject.SetActive(false);
        simulationSetUpScreen.gameObject.SetActive(true);
        lastGeneration = 0;
    }

    void LateUpdate()
    {
        if (lastGeneration != SimulationManager.Instance.Generation)
        {
            lastGeneration = SimulationManager.Instance.Generation;
            generationsText.text = String.Format(generationsString, SimulationManager.Instance.Generation);
            bestFitnessText.text = String.Format(bestFitnessString, SimulationManager.Instance.BestFitness);
            averageFitnessText.text = String.Format(averageFitnessString, Mathf.Round(SimulationManager.Instance.AverageFitness));
            worstFitnessText.text = String.Format(worstFitnessString, SimulationManager.Instance.WorstFitness);
        }
    }
}