using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        InitPool();
    }

    private void InitPool()
    {
        for (int i = 0; i < MinimumPoolCount; i++)
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
