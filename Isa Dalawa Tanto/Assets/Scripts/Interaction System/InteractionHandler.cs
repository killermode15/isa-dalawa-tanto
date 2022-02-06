using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public delegate void Interact();
    public event Interact OnInteractObj;
    public KeyCode interactButton;

    private void Update()
    {
        if(Input.GetKeyDown(interactButton))
        {
            OnInteractObj?.Invoke();
        }
    }
}
