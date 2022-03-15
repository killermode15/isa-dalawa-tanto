using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class BaseInteractable : MonoBehaviour
{
    [SerializeField] protected string name;
    [SerializeField] protected BaseInteraction interaction;
    [SerializeField] protected UnityEvent onInteract;
    [SerializeField] protected UnityEvent onInteractEnd;

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


    public virtual void OnInteract()
    {
        onInteract?.Invoke();
    }

    public virtual void OnInteractEnd()
    {
        onInteractEnd?.Invoke();
    }

    protected void OnValidate()
    {
        detectionCollider = GetComponent<CircleCollider2D>();
        detectionCollider.isTrigger = true;
        detectionCollider.radius = detectionRange;
    }
}
