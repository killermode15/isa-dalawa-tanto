using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class OnTriggerInteraction : BaseInteraction
{
    [SerializeField] private bool isTriggered = false;
    [SerializeField] private bool isOneShot = true;

    private bool hasBeenTriggeredOnce = false;
    private CircleCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    public override bool Interact()
    {
        if (isOneShot)
        {
            if (isTriggered && !hasBeenTriggeredOnce)
            {
                hasBeenTriggeredOnce = true;
                return true;
            }
            return false;
        }
        return isTriggered;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        isTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        isTriggered = false;
        hasBeenTriggeredOnce = false;
    }
}
