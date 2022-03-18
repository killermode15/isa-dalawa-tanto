using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteraction : MonoBehaviour
{
    [SerializeField] protected bool isOneShot = true;
    protected bool hasBeenTriggeredOnce = false;

    public abstract bool Interact();
}
