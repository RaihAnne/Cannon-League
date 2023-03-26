using UnityEngine;
using Unity.Netcode;

public class Cannon : NetworkBehaviour
{
    private Vector2 fireDirection;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private Transform BarrelTransform;
    [SerializeField] private Transform PlatformTransform;

    [SerializeField] private Bullet bulletPrefab;

    private void OnEnable()
    {
        fireDirection = this.gameObject.transform.up;
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        Aim(inputHandler.GetAimDirection());

        if (!inputHandler.IsFiring())
        {
            return;
        }
        Fire();
    }

    private void Aim(Vector2 aimDirection)
    {
        BarrelTransform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, aimDirection));
        fireDirection = aimDirection;
    }

    private void Fire()
    {
        var bullet = GetBullet();

        bullet.Launch(fireDirection);
    }

    private Bullet GetBullet()
    {
        var someBullet = Instantiate(bulletPrefab.gameObject);
        someBullet.transform.position = BarrelTransform.position;
        
        var bullet = someBullet.GetComponent<Bullet>();
        return bullet;
    }

    
}
