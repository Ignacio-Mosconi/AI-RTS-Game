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
    [SerializeField] TMP_Text[] minerTanksSimulationSettingTexts = new TMP_Text[(int)MinerTanksSimulationSetting.Count];

    [Header("Settings Sliders")]
    [SerializeField] Slider[] neuralNetworkSettingsSliders = new Slider[(int)SimulationSetting.Count];
    [SerializeField] Slider[] simulationSettingsSliders = new Slider[(int)SimulationSetting.Count];
    [SerializeField] Slider[] minerTanksSimulationSettingsSliders = new Slider[(int)SimulationSetting.Count];

    [Header("Simulation Start")]
    [SerializeField] Button startButton = default;
    [SerializeField] GameObject simulationScreen = default;

    string[] neuralNetworkSettings;
    string[] simulationSettings;
    string[] minerTanksSimulationSettings;

    void OnValidate()
    {
        Array.Resize(ref neuralNetworkSettingsTexts, (int)NeuralNetworkSetting.Count);
        Array.Resize(ref neuralNetworkSettingsSliders, (int)NeuralNetworkSetting.Count);
        Array.Resize(ref simulationSettingsTexts, (int)SimulationSetting.Count);
        Array.Resize(ref simulationSettingsSliders, (int)SimulationSetting.Count);
        Array.Resize(ref minerTanksSimulationSettingTexts, (int)MinerTanksSimulationSetting.Count);
        Array.Resize(ref minerTanksSimulationSettingsSliders, (int)MinerTanksSimulationSetting.Count);
    }

    void Start()
    {
        for (int i = 0; i < (int)NeuralNetworkSetting.Count; i++)
            neuralNetworkSettingsSliders[i].onValueChanged.AddListener((value) => OnNeuralNetworkSettingChange((NeuralNetworkSetting)i, value));
        for (int i = 0; i < (int)SimulationSetting.Count; i++)
            simulationSettingsSliders[i].onValueChanged.AddListener((value) => OnSimulationSettingChange((SimulationSetting)i, value));
        for (int i = 0; i < (int)MinerTanksSimulationSetting.Count; i++)
            minerTanksSimulationSettingsSliders[i].onValueChanged.AddListener((value) => 
                                                                    OnMinerTanksSimulationSettingChange((MinerTanksSimulationSetting)i, value));        

        startButton.onClick.AddListener(OnStartButtonClick);
    }

    void OnNeuralNetworkSettingChange(NeuralNetworkSetting neuralNetworkSetting, float value)
    {
        int index = (int)neuralNetworkSetting;

        switch (neuralNetworkSetting)
        {
            case NeuralNetworkSetting.NumberOfHiddenLayers:
                SimulationManager.Instance.MutationProbability = value;
                break;

            case NeuralNetworkSetting.NeuronsPerHiddenLayer:
                SimulationManager.Instance.PercentageOfElites = value;
                break;

            case NeuralNetworkSetting.Bias:
                SimulationManager.Instance.GenerationDuration = value;
                break;

            case NeuralNetworkSetting.Slope:
                SimulationManager.Instance.MutationIntensity = value;
                break;
        }

        neuralNetworkSettingsTexts[index].text = String.Format(neuralNetworkSettings[index], value);
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

        simulationSettingsTexts[index].text = String.Format(simulationSettings[index], value);
    }

    void OnMinerTanksSimulationSettingChange(MinerTanksSimulationSetting minerTanksSimulationSetting, float value)
    {
        int index = (int)minerTanksSimulationSetting;

        switch (minerTanksSimulationSetting)
        {
            case MinerTanksSimulationSetting.NumberOfMines:
                MinerTanksSimulationManager.Instance.NumberOfMines = (int)value;
                break;
        }

        simulationSettingsTexts[index].text = String.Format(simulationSettings[index], value);
    }


    void OnStartButtonClick()
    {
        SimulationManager.Instance.StartSimulation();
        gameObject.SetActive(false);
        simulationScreen.SetActive(true);
    }
}