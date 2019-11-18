using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GreenNacho.AI.NeuralNetworking;

public class MinerTanksSimulationSetUpScreen : SimulationSetUpScreen
{
    [Header("Settings Texts")]
    [SerializeField] TMP_Text numberOfMinesText = default;

    [Header("Settings Sliders")]
    [SerializeField] Slider numberOfMinesSlider = default;

    string numberOfMinesString;

    protected override void OnStart()
    {
        base.OnStart();

        numberOfMinesString = numberOfMinesText.text;
        numberOfMinesSlider.onValueChanged.AddListener((value) => OnNumberOfMinesChange(value));
        OnNumberOfMinesChange(numberOfMinesSlider.value);
    }

    void OnNumberOfMinesChange(float value)
    {
        MinerTanksSimulationManager.Instance.NumberOfMines = (int)value;

        string format = GetValueFormat(value);
        numberOfMinesText.text = String.Format(numberOfMinesString, value.ToString(format));
    }
}