using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Singleton;
    [SerializeField] private int MinimumPoolCount;
    [SerializeField] private Bullet BulletPrefab;
    [SerializeField] private Transform bulletPoolTransform;

    Queue<Bullet> InactiveBullets = new Queue<Bullet>();

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

    public Bullet GetBullet()
    {
        if (InactiveBullets.Count < 1)
        {
            MakeNewInactiveBullet();
        }

        var newBullet = InactiveBullets.Dequeue();
        //newBullet.GetComponent<NetworkObject>().Spawn();

        return newBullet;
    }

    public void Release(Bullet someBullet)
    {
        someBullet.gameObject.SetActive(false);
        InactiveBullets.Enqueue(someBullet);
    }

    private void MakeNewInactiveBullet()
    {
        var NewBullet = Instantiate<Bullet>(BulletPrefab, bulletPoolTransform);
        InactiveBullets.Enqueue(NewBullet);
    }

}
