using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] private Wall LeftWall;
    [SerializeField] private Wall RightWall;
    [SerializeField] private Wall TopWall;
    [SerializeField] private Wall BottomWall;

    [SerializeField] private Camera MainCamera;

    private void Awake()
    {
        SetupWall();
    }

    private void SetupWall()
    {
        LeftWall.transform.position = GetScreenEdgePosition(Vector2.left);
        RightWall.transform.position = GetScreenEdgePosition(Vector2.right);
        TopWall.transform.position = GetScreenEdgePosition(Vector2.up);

        var newPos = GetScreenEdgePosition(Vector2.down);
        newPos.y += 3;
        BottomWall.transform.position = newPos;

    }

    private Vector2 GetScreenEdgePosition(Vector2 screenEdge)
    {
        if (screenEdge == Vector2.up)
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height, 0));
        }

        if (screenEdge == Vector2.down)
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0));
        }

        if (screenEdge == Vector2.right)
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 0));
        }

        if (screenEdge == Vector2.left)
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 0));
        }

        return Vector2.zero;
    }
}
