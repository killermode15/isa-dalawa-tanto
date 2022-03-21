using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Transform sprite;
    [SerializeField] private Animator animator;
    [SerializeField] private MovementInput movementInput;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Vector3 crouchPosition;
    [SerializeField] private Vector3 originalScale;
    [SerializeField] private Vector3 crouchScale;

    private void Start()
    {
        movementInput = this.GetComponent<MovementInput>();

        originalScale = sprite.localScale;
        crouchScale = sprite.localScale * 0.66667f;
        originalPosition = sprite.localPosition;
        //crouchPosition -= crouchScale;
    }

    private void Update()
    {
        if (movementInput.Horizontal != 0)
        {
            animator.SetBool("idle", false);
            animator.SetBool("jumping", false);
            animator.SetBool("walking", true);
        }

        if (movementInput.HasJumped && !characterController.Grounded)
        {
            animator.SetBool("idle", false);
            animator.SetBool("jumping", true);
            animator.SetBool("walking", false);
        }

        if (movementInput.IsHoldingJump && !characterController.Grounded)
        {
            animator.SetBool("idle", false);
            animator.SetBool("jumping", true);
            animator.SetBool("walking", false);
        }

        if (movementInput.Horizontal == 0 && !movementInput.HasJumped && characterController.Grounded)
        {
            animator.SetBool("idle", true);
            animator.SetBool("jumping", false);
            animator.SetBool("walking", false);
        }

        if (movementInput.IsHoldingCrouch && sprite.localScale != crouchScale)
        {
            sprite.localScale = crouchScale;
            sprite.transform.localPosition = new Vector3(0, crouchPosition.y, 0);
        }

        if (!movementInput.IsHoldingCrouch && sprite.localScale != originalScale)
        {
            sprite.localScale = originalScale;
            sprite.transform.localPosition = new Vector3(0, originalPosition.y, 0);
        }
    }
}
