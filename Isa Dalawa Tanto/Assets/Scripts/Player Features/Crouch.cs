using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class Crouch : MonoBehaviour
{
    [SerializeField] private Transform playerSprite;
    [SerializeField] private Vector3 crouchScale;
    [SerializeField] private Bounds crouchBounds;
    [SerializeField] private KeyCode crouchButton;

    private PlayerController playerController;
    private Vector3 defaultPos;
    private Vector3 defaultScale;
    private Bounds defaultBounds;

    private bool canCrouch = true;

    private void Awake()
    {
        playerController = this.GetComponent<PlayerController>();

        defaultScale = playerSprite.transform.localScale;
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
            playerSprite.transform.localScale = crouchScale;
            Debug.Log("Player is crouching");
        }
        else
        {
            this.transform.localPosition = defaultPos;
            playerController.CharacterBounds = defaultBounds;
            playerSprite.transform.localPosition = Vector3.zero;
            playerSprite.transform.localScale = defaultScale;
            Debug.Log("Player is standing");
        }
    }
}
