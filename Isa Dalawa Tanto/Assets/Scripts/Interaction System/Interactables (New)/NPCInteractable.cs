using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : BaseInteractable
{
    [Space]
    [Header("NPC Parameters")]
    public int dialogueId;

    public override void OnInteract()
    {
        base.OnInteract();
        DialogueController.Instance.TriggerDialogue(dialogueId);
        Debug.Log("Loading dialogue");
        //NOTE: Disable character movement here
    }
}
