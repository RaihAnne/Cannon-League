using System;
using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class NetworkManagerCustomEvents : MonoBehaviour
{
    public static Action OnFinishedInitEvent;
    public static Action<ulong> OnClientConnectedEvent;

    private void OnEnable()
    {
        StartCoroutine(WaitForInit());
    }

    IEnumerator WaitForInit()
    {
        while (NetworkManager.Singleton == null)
        {
            yield return null;
        }

        NetworkManager.Singleton.OnClientConnectedCallback += NotifyListeners;

        if (OnFinishedInitEvent != null)
        {
            OnFinishedInitEvent();
        }
    }

    private void NotifyListeners(ulong clientID)
    {
        if (OnClientConnectedEvent != null)
        {
            OnClientConnectedEvent(clientID);
        }
    }
}
