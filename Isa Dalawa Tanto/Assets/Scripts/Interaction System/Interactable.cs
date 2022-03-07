using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    // public string Name { get { return name; } set { name = value; } }
    [SerializeField] private string name;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private TextMeshPro interactButtonTxt;

    [SerializeField] private Vector2 detectionRadius;

    private bool registeredInteraciton;

    protected InteractionHandler handler;

    private void OnValidate()
    {
        SetDetectionRadius(detectionRadius);
    }

    private void Start()
    {
        ToggleUI(false);
    }

    protected virtual void SetDetectionRadius(Vector2 size)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Interact detected");
        if (col.gameObject.GetComponent<InteractionHandler>())
        {
            handler = col.gameObject.GetComponent<InteractionHandler>();

            interactButtonTxt.text = handler.interactButton.ToString();

            if (!registeredInteraciton)
            {
                handler.OnInteractObj += Interact;
                registeredInteraciton = true;
            }

            Debug.Log("Interact registered");
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (handler)
        {
            interactUI.SetActive(true);

            //To Do: Tell sean to tie the boolean upon closing dialogue
            if(!registeredInteraciton)
            {
                handler.OnInteractObj += Interact;
                registeredInteraciton = true;
                Debug.Log("Interact registered");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Interact lost");
        if (col.gameObject.GetComponent<InteractionHandler>())
        {
            handler = col.gameObject.GetComponent<InteractionHandler>();

            ToggleUI(false);
            handler.OnInteractObj -= Interact;
            registeredInteraciton = false;
            handler.enabled = true;

            Debug.Log("Interact unregistered");
        }
    }

    protected virtual void Interact()
    {
        Debug.Log("Interacting with " + name);
        ToggleUI(false);
        handler.enabled = false;
    }

    public void ToggleUI(bool state)
    {
        interactUI.SetActive(state);
    }
}
