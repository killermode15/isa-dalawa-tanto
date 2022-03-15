using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;

    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    public float Horizontal => horizontal;
    public float Vertical => vertical;
    public bool IsHoldingCrouch => isCrouching;
    public bool HasJumped => hasJumped;
    public bool IsHoldingJump => isHoldingJump;

    private float horizontal;
    private float vertical;
    private bool isCrouching;
    private bool hasJumped;
    private bool isHoldingJump;

    // Update is called once per frame
    void Update()
    {
        if (!isEnabled)
        {
            horizontal = vertical = 0;
            isCrouching = false;
            hasJumped = false;
            return; 
        }
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        isCrouching = Input.GetKey(crouchKey);

        hasJumped = Input.GetKeyDown(jumpKey);
        isHoldingJump = Input.GetKey(jumpKey);

    }

    public void Enabled(bool state)
    {
        isEnabled = state;
    }
}
