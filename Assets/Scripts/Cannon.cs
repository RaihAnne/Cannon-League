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
        Aim(inputHandler.GetXInput());

        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }
        Fire();
    }

    private void Aim(float xInput)
    {
        float angle = Mathf.Acos(xInput) * 180 / Mathf.PI;
        BarrelTransform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        fireDirection.x = Mathf.Cos(angle * Mathf.PI/180);
        fireDirection.y = Mathf.Sin(angle * Mathf.PI/180);
    }

    private void Fire()
    {
        var bulletObject = Instantiate(bulletPrefab.gameObject);
        bulletObject.transform.position = BarrelTransform.position;

        var bullet = bulletObject.GetComponent<Bullet>();

        bullet.Launch(fireDirection);
    }




}
