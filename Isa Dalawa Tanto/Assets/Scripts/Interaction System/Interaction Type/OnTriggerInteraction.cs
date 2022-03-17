using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnTriggerInteraction : BaseInteraction
{
    [SerializeField] private bool isTriggered = false;

    private Collider2D collider;

    private void Awake()
    {
        //collider = GetComponent<CircleCollider2D>();
        collider = GetComponent<Collider2D>();
        if (!collider)
            Debug.LogError("This Trigger Interaction does not contain a trigger collider", this);

        if (collider)
            collider.isTrigger = true;
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
    }
}
