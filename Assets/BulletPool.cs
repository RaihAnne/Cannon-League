using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class BulletPool : NetworkBehaviour, INetworkPrefabInstanceHandler
{
    public static BulletPool Singleton;
    [SerializeField] private int MinimumPoolCount;
    [SerializeField] private Bullet BulletPrefab;
    [SerializeField] private Transform bulletPoolTransform;

    Queue<Bullet> InactiveBullets = new Queue<Bullet>();

    
    public override void OnNetworkSpawn()
    {
        NetworkManager.PrefabHandler.AddHandler(BulletPrefab.gameObject, this);
    }

    private void Awake()
    {
        Singleton = this;
        NetworkManagerCustomEvents.OnClientConnectedEvent += OnClientConnected;
    }

    private void OnClientConnected(ulong obj)
    {
        InitPool();
    }

    public void InitPool()
    {
        for (int i = InactiveBullets.Count; i < MinimumPoolCount; i++)
        {
            MakeNewInactiveBullet();
        }
    }

    public Bullet GetBullet(Vector3 position)
    {
        if (InactiveBullets.Count < 1)
        {
            MakeNewInactiveBullet();
        }

        var newBullet = InactiveBullets.Dequeue();
        newBullet.transform.SetPositionAndRotation(position, Quaternion.identity);
        newBullet.gameObject.SetActive(true);

        var networBullet = newBullet.GetComponent<NetworkObject>();
        networBullet.Spawn();

        return newBullet;
    }

    public void Release(NetworkObject networkBullet)
    {
        networkBullet.Despawn();
    }

    private void AddBulletBackToPool(Bullet someBullet)
    {
        someBullet.gameObject.SetActive(false);
        InactiveBullets.Enqueue(someBullet);
    }

    private void MakeNewInactiveBullet()
    {
        var NewBullet = Instantiate<Bullet>(BulletPrefab);
        InactiveBullets.Enqueue(NewBullet);
    }

    public void Destroy(NetworkObject networkBullet)
    {
        var bullet = networkBullet.GetComponent<Bullet>();
        AddBulletBackToPool(bullet);
    }

    public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
    {
        var bulletObject = GetBullet(position);
        var NetworkBullet = bulletObject.GetComponent<NetworkObject>();
        return NetworkBullet;
    }
}


