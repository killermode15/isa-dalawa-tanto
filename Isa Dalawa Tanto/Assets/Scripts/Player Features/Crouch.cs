using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class Crouch : MonoBehaviour
{
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Bounds crouchBounds;
    [SerializeField] private KeyCode crouchButton;
    [SerializeField] private float crouchScale;

    private PlayerController playerController;
    private Bounds defaultBounds;
    private BoxCollider2D collider;

    private bool canCrouch = true;

    private void Awake()
    {
        playerController = this.GetComponent<PlayerController>();
        collider = this.GetComponent<BoxCollider2D>();
        defaultBounds = playerController.CharacterBounds;
    }

    private void Update()
    {
        if (Input.GetKeyDown(crouchButton))
        {
            ResizePlayer(true);
        }

        if (Input.GetKeyUp(crouchButton))
        {
            ResizePlayer();
        }

    }

    private void ResizePlayer(bool state = false)
    {
        //To do: player cannot stand if there is no head room
        if (state)
        {
            playerController.CharacterBounds = crouchBounds;
            this.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y / crouchScale);
            collider.size = new Vector2(collider.size.x, collider.size.y / crouchScale);
            collider.offset = new Vector2(collider.offset.x, collider.offset.y / crouchScale);
        }

        else
        {
            Debug.Log(playerController.CollisionUp);

            if (!playerController.CollisionUp)
            {
                playerController.CharacterBounds = defaultBounds;
                this.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * crouchScale);
                collider.size = new Vector2(collider.size.x, collider.size.y * crouchScale);
                collider.offset = new Vector2(collider.offset.x, collider.offset.y * crouchScale);
            }
        }
    }
}
