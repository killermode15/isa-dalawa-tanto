using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : BaseInteractable
{
    [SerializeField] private int dialogueId = 0;
            
    public override void OnInteract()
    {
        base.OnInteract();
        DialogueController.Instance.TriggerDialogue(dialogueId);
    }
}
