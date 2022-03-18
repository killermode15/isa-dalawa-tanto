using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInteractable : BaseInteractable
{
    private FadeUI fadeScript;
    [SerializeField] private bool shouldFadeToBlack;

    private void Start()
    {
        fadeScript = FadeUI.Instance;
    }

    public override void OnInteract()
    {
        base.OnInteract();
        fadeScript.Fade(shouldFadeToBlack);
        fadeScript.OnFadeEnd.AddListener(OnInteractEnd);
    }

    public override void OnInteractEnd()
    {
        base.OnInteractEnd();
        fadeScript.OnFadeEnd.RemoveListener(OnInteractEnd);
    }
}
