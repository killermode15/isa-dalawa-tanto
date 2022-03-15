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
            return;
        if(Interaction.Interact())
        {
            Interactable.OnInteract();
        }
    }

    public void SetInteraction(BaseInteraction interaction, BaseInteractable interactable)
    {
        Interaction = interaction;
        Interactable = interactable;
    }
}
