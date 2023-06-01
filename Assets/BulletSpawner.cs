using Unity.Netcode;
using UnityEngine;

public class BulletSpawner : NetworkBehaviour
{
    [SerializeField] NetworkObject BulletObject;

    public static BulletSpawner Singleton;

    bool HasGameStarted = false;

    private void Awake()
    {
        Singleton = this;
        GameCountdownHandler.OnCountdownFinishedEvent += () =>
        {
            HasGameStarted = true;
        };

        GameManager.OnGameEndEvent += () =>
        {
            HasGameStarted = false;
        };
    }

    [ServerRpc(RequireOwnership = false)]
    public void GetAndFireBulletServerRpc(Vector2 spawnLocation,Vector2 fireDirection)
    {
        if (!HasGameStarted)
        {
            return;
        }

        var networkBullet = Instantiate(BulletObject, spawnLocation, Quaternion.identity);
        networkBullet.Spawn();

        var bullet = networkBullet.GetComponent<Bullet>();
        bullet.Launch(fireDirection);
    }

}
