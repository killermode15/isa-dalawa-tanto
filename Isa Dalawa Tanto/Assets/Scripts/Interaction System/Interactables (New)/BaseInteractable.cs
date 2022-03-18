using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class BaseInteractable : MonoBehaviour
{
    [SerializeField] protected GameObject interactKeyUI;
    [SerializeField] protected SpriteRenderer spriteToChange;
    [SerializeField] protected Sprite interactableSprite;
    [SerializeField] protected BaseInteraction interaction;
    [SerializeField] protected UnityEvent onInteract;
    [SerializeField] protected UnityEvent onInteractEnd;

    [Header("Detection Parameters")]
    [SerializeField] protected float detectionRange = 1;

    protected GameObject detectedPlayer;
    protected CircleCollider2D detectionCollider;

    protected Sprite defaultSprite;

    protected void Awake()
    {
        detectionCollider = GetComponent<CircleCollider2D>();
        detectionCollider.isTrigger = true;
        detectionCollider.radius = detectionRange;

        //if interaction subtype is KeypressInteractable 
        if(spriteToChange)
            defaultSprite = spriteToChange.sprite;

        if (interaction.GetType() == typeof(KeyPressInteraction) && interactKeyUI != null)
        {            
            KeyPressInteraction keyPressInteraction = interaction as KeyPressInteraction;
            interactKeyUI.GetComponentInChildren<TMP_Text>().text = keyPressInteraction.GetInteractKey;
            interactKeyUI.SetActive(false);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GameObject player = collision.gameObject;
        InteractionHandler handler = player.GetComponent<InteractionHandler>();

        if (!handler)
            return;

        if (interactKeyUI)
        {
            KeyPressInteraction interactionType = interaction as KeyPressInteraction; 

            if (spriteToChange)
                spriteToChange.sprite = interactableSprite;
            
            interactKeyUI.GetComponentInChildren<TMP_Text>().text = interactionType.GetInteractKey;
            interactKeyUI.SetActive(true);
        }

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

        if (interactKeyUI)
        {
            if (spriteToChange)
                spriteToChange.sprite = defaultSprite;

            interactKeyUI.SetActive(false);
        }
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
