using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressInteraction : BaseInteraction
{
    [SerializeField] private KeyCode keyCode = KeyCode.F;

    public string GetInteractKey => keyCode.ToString();
    public override bool Interact()
    {
        return Input.GetKeyDown(keyCode);
    }
}
