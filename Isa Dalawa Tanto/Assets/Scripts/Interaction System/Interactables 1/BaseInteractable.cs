using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class BaseInteractable : MonoBehaviour
{
    [SerializeField] protected string name;
    [SerializeField] protected BaseInteraction interaction;

    [Header("Detection Parameters")]
    [SerializeField] protected float detectionRange = 1;

    protected GameObject detectedPlayer;
    protected CircleCollider2D detectionCollider;

    protected void Awake()
    {
        detectionCollider = GetComponent<CircleCollider2D>();
        detectionCollider.isTrigger = true;
        detectionCollider.radius = detectionRange;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GameObject player = collision.gameObject;
        InteractionHandler handler = player.GetComponent<InteractionHandler>();

        if (!handler)
            return;

        handler.SetInteraction(interaction, this);
        detectedPlayer = player;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GameObject player = collision.gameObject;
        InteractionHandler handler = player.GetComponent<InteractionHandler>();

        if (!handler)
            return;

        handler.SetInteraction(null, null);
        detectedPlayer = null;

    }


    public abstract void OnInteract();

    protected void OnValidate()
    {
        detectionCollider = GetComponent<CircleCollider2D>();
        detectionCollider.isTrigger = true;
        detectionCollider.radius = detectionRange;
    }
}
