using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected BaseView view;
    protected BaseModel model;

    protected bool isInitialized = false;
    protected void SetupController<V, M>(V view, M model) where V : BaseView where M : BaseModel
    {
        this.view = view;
        this.model = model;

        isInitialized = true;
    }

    // Inefficient but good enough for small games
    protected void SetupController<V, M>() where V : BaseView where M : BaseModel
    {
        view = FindObjectOfType<V>();
        model = FindObjectOfType<M>();

        isInitialized = true;
    }
}
