using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public delegate void Interact();
    public event Interact OnInteractObj;

    private BaseInteraction Interaction;
    private BaseInteractable Interactable;

    private void Update()
    {
        if (!Interaction)
        {
            Debug.Log("No interaction script exists");
            return;
        }
        if (Interaction.Interact())
        {
            Debug.Log("Interacting");
            Interactable.OnInteract();
        }
        else
            Debug.Log("Can't interact");
    }

    public void SetInteraction(BaseInteraction interaction, BaseInteractable interactable)
    {
        Interaction = interaction;
        Interactable = interactable;
    }
}
