using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : Interactable
{  
    public int dialogueId;

    protected override void Interact()
    {
        base.Interact();
        
        DialogueController.Instance.TriggerDialogue(dialogueId);
        Debug.Log("Loading dialogue");
    }
}
