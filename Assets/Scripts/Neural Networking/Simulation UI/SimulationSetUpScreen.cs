using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GreenNacho.AI.NeuralNetworking;

public enum NeuralNetworkSetting
{
    NumberOfHiddenLayers,
    NeuronsPerHiddenLayer,
    Bias,
    Slope,
    Count
}

public enum SimulationSetting
{
    PopulationAmount,
    GenerationDuration,
    PercentageOfElites,
    MutationProbability,
    MutationIntensity,
    Count
}

public enum MinerTanksSimulationSetting
{
    NumberOfMines,
    Count
}

public class SimulationSetUpScreen : MonoBehaviour
{
    [Header("Settings Texts")]
    [SerializeField] TMP_Text[] neuralNetworkSettingsTexts = new TMP_Text[(int)NeuralNetworkSetting.Count];
    [SerializeField] TMP_Text[] simulationSettingsTexts = new TMP_Text[(int)SimulationSetting.Count];

    [Header("Settings Sliders")]
    [SerializeField] Slider[] neuralNetworkSettingsSliders = new Slider[(int)SimulationSetting.Count];
    [SerializeField] Slider[] simulationSettingsSliders = new Slider[(int)SimulationSetting.Count];

    [Header("Simulation Start")]
    [SerializeField] Button startButton = default;
    [SerializeField] SimulationInfoScreen simulationInfoScreen = default;

    string[] neuralNetworkSettingsStrings = new string[(int)NeuralNetworkSetting.Count];
    string[] simulationSettingsStrings = new string[(int)SimulationSetting.Count];

    void OnValidate()
    {
        Array.Resize(ref neuralNetworkSettingsTexts, (int)NeuralNetworkSetting.Count);
        Array.Resize(ref neuralNetworkSettingsSliders, (int)NeuralNetworkSetting.Count);
        Array.Resize(ref simulationSettingsTexts, (int)SimulationSetting.Count);
        Array.Resize(ref simulationSettingsSliders, (int)SimulationSetting.Count);
    }

    void Start()
    {
        OnStart();
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    protected virtual void OnStart()
    {
        for (int i = 0; i < (int)NeuralNetworkSetting.Count; i++)
        {
            NeuralNetworkSetting neuralNetworkSetting = (NeuralNetworkSetting)i;

            neuralNetworkSettingsStrings[i] = neuralNetworkSettingsTexts[i].text;
            neuralNetworkSettingsSliders[i].onValueChanged.AddListener((value) => OnNeuralNetworkSettingChange(neuralNetworkSetting, value));
            OnNeuralNetworkSettingChange(neuralNetworkSetting, neuralNetworkSettingsSliders[i].value);
        }

        for (int i = 0; i < (int)SimulationSetting.Count; i++)
        {
            SimulationSetting simulationSetting = (SimulationSetting)i;

            simulationSettingsStrings[i] = simulationSettingsTexts[i].text;
            simulationSettingsSliders[i].onValueChanged.AddListener((value) => OnSimulationSettingChange(simulationSetting, value));
            OnSimulationSettingChange(simulationSetting, simulationSettingsSliders[i].value);
        }
    }

    protected string GetValueFormat(float value)
    {
        return ((Mathf.Round(value) != value) ? "0.##" : "");
    }

    void OnNeuralNetworkSettingChange(NeuralNetworkSetting neuralNetworkSetting, float value)
    {
        int index = (int)neuralNetworkSetting;

        switch (neuralNetworkSetting)
        {
            case NeuralNetworkSetting.NumberOfHiddenLayers:
                SimulationManager.Instance.HiddenLayers = (int)value;
                break;

            case NeuralNetworkSetting.NeuronsPerHiddenLayer:
                SimulationManager.Instance.NeuronsPerHiddenLayer = (int)value;
                break;

            case NeuralNetworkSetting.Bias:
                SimulationManager.Instance.Bias = value;
                break;

            case NeuralNetworkSetting.Slope:
                SimulationManager.Instance.Slope = value;
                break;
        }

        string format = GetValueFormat(value);
        
        neuralNetworkSettingsTexts[index].text = String.Format(neuralNetworkSettingsStrings[index], value.ToString(format));
    }

    void OnSimulationSettingChange(SimulationSetting simulationSetting, float value)
    {
        int index = (int)simulationSetting;

        switch (simulationSetting)
        {
            case SimulationSetting.GenerationDuration:
                SimulationManager.Instance.GenerationDuration = value;
                break;

            case SimulationSetting.MutationIntensity:
                SimulationManager.Instance.MutationIntensity = value;
                break;

            case SimulationSetting.MutationProbability:
                SimulationManager.Instance.MutationProbability = value;
                break;

            case SimulationSetting.PercentageOfElites:
                SimulationManager.Instance.PercentageOfElites = value;
                break;

            case SimulationSetting.PopulationAmount:
                SimulationManager.Instance.PopulationAmount = (int)value;
                break;
        }

        string format = GetValueFormat(value);

        simulationSettingsTexts[index].text = String.Format(simulationSettingsStrings[index], value.ToString(format));
    }

    void OnStartButtonClick()
    {
        SimulationManager.Instance.StartSimulation();
        gameObject.SetActive(false);
        simulationInfoScreen.gameObject.SetActive(true);
    }
}