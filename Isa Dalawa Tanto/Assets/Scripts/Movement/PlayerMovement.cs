using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementInput input;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchScale = 0.5f;
    [SerializeField] private bool IsCrouching = false;

    [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float midAirSpeedBonus = 2;

    [Header("Jump Parameters")]
    [SerializeField] private LayerMask ground;
    [SerializeField] private float jumpVelocity = 8;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2.0f;

    [Header("Gravity Parameters")]
    [SerializeField] private float gravity = -9.8f;

    private Rigidbody2D rb;
    private float originalScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Crouch();
        ApplyMovement();

        if (input.HasJumped && IsGrounded())
        {
            Jump();
        }
        UpdateJumpVelocity();
    }

    private void Crouch()
    {
        if(input.IsHoldingCrouch && !IsCrouching)
        {
            originalScale = transform.localScale.y;
            transform.localScale = new Vector3(1, crouchScale, 1);
            transform.position -= new Vector3(0, crouchScale / 2, 0);

            IsCrouching = true;
        }

        if(!input.IsHoldingCrouch && IsCrouching)
        {
            transform.localScale = new Vector3(1, originalScale, 1);
            transform.position += new Vector3(0, originalScale/2 - crouchScale/2, 0);

            IsCrouching = false;
        }
    }

    private void ApplyMovement()
    {
        float move = input.Horizontal * moveSpeed * Time.deltaTime;

        //if (move < 0 && isColldingAt(Vector2.left) || move > 0 && isColldingAt(Vector2.right))
        //    move = 0;

        if (!IsGrounded())
        {
            move += ((input.Horizontal * midAirSpeedBonus) / 2) * Time.deltaTime;
        }

        transform.position += new Vector3(move, 0);
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpVelocity;
    }

    private void UpdateJumpVelocity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !input.IsHoldingJump)
        {
            rb.velocity += Vector2.up * gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        Vector3 startPos = transform.position;
        startPos.y -= transform.localScale.y / 2 + 0.025f;

        bool left = Physics2D.Raycast(startPos + new Vector3(-transform.localScale.x / 2, 0), Vector2.down, 0.1f, ground);
        bool center = Physics2D.Raycast(startPos, Vector2.down, 0.1f, ground);
        bool right = Physics2D.Raycast(startPos + new Vector3(transform.localScale.x / 2, 0), Vector2.down, 0.1f, ground);


        return left || center || right;
    }

    private void OnDrawGizmos()
    {
        Vector3 startPos = transform.position;
        startPos.y -= transform.localScale.y / 2 + 0.025f;



        Gizmos.color = Color.green;

        Gizmos.DrawRay(startPos + new Vector3(-transform.localScale.x / 2, 0), Vector2.down * 0.1f);
        Gizmos.DrawRay(startPos + new Vector3(transform.localScale.x / 2, 0), Vector2.down * 0.1f);
        Gizmos.DrawRay(startPos, Vector2.down * 0.1f);
    }
}
