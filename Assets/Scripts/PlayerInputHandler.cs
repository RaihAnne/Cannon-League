using Unity.Netcode;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Transform CannonTransform;

    private Camera MainCamera;
    private Vector2 aimDirection;
    private bool isFiring = false;

    private void Awake()
    {
        MainCamera = FindObjectOfType<Camera>();
    }

    public Vector2 GetAimDirection()
    {
        var pointA = CannonTransform.position;
        var pointB = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        aimDirection = pointB - pointA;

        if (Mathf.Approximately(aimDirection.x, 0))
        {
            aimDirection.x = 0;
        }

        if (Mathf.Approximately(aimDirection.y, 0))
        {
            aimDirection.y = 0;
        }

        return aimDirection.normalized;
    }

    public bool IsFiring()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
