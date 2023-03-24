using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Vector2 fireDirection;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private Transform BarrelTransform;
    [SerializeField] private Transform PlatformTransform;

    [SerializeField] private Bullet bulletPrefab;

    private void OnEnable()
    {
        fireDirection = this.gameObject.transform.up;
        Debug.Log("fireDirection = " + fireDirection);
    }

    private void Update()
    {
        Aim(inputHandler.GetAimDirection());

        if (!Input.GetKeyDown(KeyCode.Space))
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
        var bulletObject = Instantiate(bulletPrefab.gameObject);
        bulletObject.transform.position = BarrelTransform.position;

        var bullet = bulletObject.GetComponent<Bullet>();

        bullet.Launch(fireDirection);
    }




}
