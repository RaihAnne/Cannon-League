using Unity.Netcode;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Transform CannonTransform;
    
    
    private FixedJoystick joystick;
    private Camera MainCamera;
    private Vector2 aimDirection;

    private void Awake()
    {
        MainCamera = FindObjectOfType<Camera>();
    }

    public Vector2 GetAimDirection()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        var pointA = CannonTransform.position;
        var pointB = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = pointB - pointA;

#elif UNITY_ANDROID && !UNITY_EDITOR
        if (joystick == null)
        {
            var touchPointA = CannonTransform.position;
            var touchPointB = GetTouchWorldPosition();
            aimDirection = touchPointB - touchPointA;
        }
        else
        {
            aimDirection = GetTouchJoystickDirection();
        }
#endif

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
        return Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 1;
    }

    public void SetJoystick(FixedJoystick localJoystick)
    {
        joystick = localJoystick;
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private Vector3 GetTouchWorldPosition()
    {
        if (Input.touchCount <= 0)
        {
            return Vector3.up;
        }
        Touch touchInput = Input.GetTouch(0);

        return MainCamera.ScreenToWorldPoint(touchInput.position);

    }

    private Vector3 GetTouchJoystickDirection()
    {
        return joystick.Direction;
    }    
#endif


}
