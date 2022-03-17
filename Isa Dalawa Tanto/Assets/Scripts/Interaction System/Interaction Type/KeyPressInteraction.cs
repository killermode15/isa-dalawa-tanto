using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressInteraction : BaseInteraction
{
    [SerializeField] private KeyCode keyCode = KeyCode.F;
    public override bool Interact()
    {
        if (isOneShot)
        {
            if (!hasBeenTriggeredOnce)
            {
                hasBeenTriggeredOnce = Input.GetKeyDown(keyCode);
                return hasBeenTriggeredOnce;
            }
            else
                return false;
        }

        return Input.GetKeyDown(keyCode);
    }
}
