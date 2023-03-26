using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private Transform[] SpawnTransforms;

    [SerializeField] private NetworkObject CannonPlatformPrefab;

    private Dictionary<Transform, bool> IsTransformOccupied = new Dictionary<Transform, bool>();
    private List<ulong> PlacedClients = new List<ulong>();

    private void Awake()
    {
        InitDictionary();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForNetworkManagerInit());
    }

    IEnumerator WaitForNetworkManagerInit()
    {
        while (NetworkManager.Singleton == null)
        {
            yield return null;
        }

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnect;
    }

    private void OnClientConnect(ulong obj)
    {
        if (!IsServer)
        {
            return;
        }
        UpdateClientPlatforms();
    }

    private void InitDictionary()
    {
        foreach (var transform in SpawnTransforms)
        {
            IsTransformOccupied.Add(transform, false);
        }
    }

    private void UpdateClientPlatforms()
    {
        var clients = NetworkManager.Singleton.ConnectedClientsIds;

        foreach (var client in clients)
        {
            if (PlacedClients.Contains(client))
            {
                continue;
            }

            var networkCannon = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(client);
            var newPosition = GetAvailableCannonPosition();
            networkCannon.gameObject.transform.SetPositionAndRotation(newPosition, Quaternion.Euler(0, 0, 0));

            PlacedClients.Add(client);

            MoveCannonClientRpc(newPosition, ClientSendParamsCache.GetSendParamsForClient(client));
        }
    }

    [ClientRpc]
    private void MoveCannonClientRpc(Vector3 toPosition, ClientRpcParams clientRpcParams)
    {
        var localCannon = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        localCannon.gameObject.transform.SetPositionAndRotation(toPosition, Quaternion.Euler(0, 0, 0));
    }

    private Vector3 GetAvailableCannonPosition()
    {
        foreach (var spawnTransform in SpawnTransforms)
        {
            if (!IsTransformOccupied[spawnTransform])
            {
                IsTransformOccupied[spawnTransform] = true;
                return spawnTransform.position;
            }
        }
        return Vector3.zero;
    }
}
