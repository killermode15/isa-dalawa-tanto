using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseModel : MonoBehaviour
{
    protected BaseController controller;

    protected bool isInitialized = false;

    protected void SetupModel<T>() where T : BaseController
    {
        controller = FindObjectOfType<T>();
        isInitialized = true;
    }

    protected void SetupModel<T>(T controller) where T : BaseController
    {
        this.controller = controller;
        isInitialized = true;
    }
}
