using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private MovementInput input;
    [SerializeField] private CharacterController controller;

    [SerializeField] private float moveSpeed;

    private float horizontalMove = 0f;
    private bool hasJumped = false;

    // Update is called once per frame
    void Update()
    {
        if (!input)
        {
            Debug.LogWarning("No movement input is set for this movement handler", this);
            return;
        }

        horizontalMove = input.Horizontal * moveSpeed;

        controller.Move(horizontalMove * Time.fixedDeltaTime, input.IsHoldingCrouch, input.HasJumped);
    }
}
