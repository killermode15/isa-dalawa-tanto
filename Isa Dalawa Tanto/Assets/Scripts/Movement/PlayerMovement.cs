using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementInput input;

    [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float acceleration = 90;
    [SerializeField] private float deceleration = 60;
    [SerializeField] private float movementClamp = 13;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpVelocity = 8;

    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2.0f;

    [Header("Gravity Parameters")]
    [SerializeField] private float gravity = -9.8f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        ApplyMovement();

        if (input.HasJumped && IsGrounded())
        {
            Jump();
        }
        UpdateJumpVelocity();
    }

    private void ApplyMovement()
    {
        float move = input.Horizontal * moveSpeed * Time.deltaTime;

        //if (move < 0 && isColldingAt(Vector2.left) || move > 0 && isColldingAt(Vector2.right))
        //    move = 0;

        transform.position += new Vector3(move, 0);
    }

    private bool isColldingAt(Vector2 dir)
    {
        Vector3 startPos = transform.position;
        if (dir.x < 0)
            startPos.x -= transform.localScale.x / 2 + 0.1f;
        else
            startPos.x += transform.localScale.x / 2 + 0.1f;
        return Physics2D.Raycast(startPos, dir, 0.1f);
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

        return Physics2D.Raycast(startPos, Vector2.down, 0.1f);
    }

    private void OnDrawGizmos()
    {
        Vector3 startPos = transform.position;
        startPos.y -= transform.localScale.y / 2;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startPos, 0.1f);

        Gizmos.DrawRay(startPos, Vector2.down * 0.1f);
    }
}
