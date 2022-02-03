using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseView : MonoBehaviour
{
    protected BaseController controller;

    protected bool isInitialized = false;

    protected void SetupView<T>() where T : BaseController
    {
        controller = FindObjectOfType<T>();
        isInitialized = true;
    }

    protected void SetupView<T>(T controller) where T : BaseController
    {
        this.controller = controller;
        isInitialized = true;
    }
}
