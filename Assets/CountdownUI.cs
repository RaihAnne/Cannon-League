using System;
using UnityEngine;
using TMPro;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;

    private void OnEnable()
    {
        GameCountdownHandler.OnCountdownFinishedEvent += OnCountdownFinished;
        GameCountdownHandler.OnTickEvent += OnCountdownTick;
    }

    private void OnDisable()
    {
        GameCountdownHandler.OnCountdownFinishedEvent += OnCountdownFinished;
        GameCountdownHandler.OnTickEvent += OnCountdownTick;
    }

    private void OnCountdownFinished()
    {
        textUI.text = "GO!";
        Invoke(nameof(ClearTextUI), 1);
    }

    private void ClearTextUI()
    {
        textUI.text = "";
    }

    private void OnCountdownTick(int value)
    {
        textUI.text = value.ToString();
    }
}
