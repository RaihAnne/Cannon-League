using System;
using UnityEngine;

public class GameCountdownHandler : MonoBehaviour
{
    [SerializeField] private int CountdownLength = 3;
    private int CountdownValue;

    public static Action<int> OnTickEvent;
    public static Action OnCountdownFinishedEvent;

    public void StartCountdown()
    {
        CountdownValue = CountdownLength;
        InvokeRepeating(nameof(OnCountdownTick), 0.1f, 1);
    }


    private void OnCountdownTick()
    {
        if (OnTickEvent != null)
        {
            OnTickEvent(CountdownValue);
        }

        if (CountdownValue <= 0)
        {
            CancelInvoke(nameof(OnCountdownTick));

            if (OnCountdownFinishedEvent != null)
            {
                OnCountdownFinishedEvent();
            }

            return;
        }

        CountdownValue--;
    }

}
