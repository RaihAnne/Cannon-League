using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private float xInput = 0;
    private bool isFiring = false;

    private void Update()
    {
        xInput = Input.GetAxis("Horizontal");

        isFiring = Input.GetButton("Fire1");
    }

    public float GetXInput()
    {
        if (Mathf.Approximately(xInput, 0))
        {
            xInput = 0;
        }

        return xInput;
    }

    public bool IsFiring()
    {
        return isFiring;
    }
}
