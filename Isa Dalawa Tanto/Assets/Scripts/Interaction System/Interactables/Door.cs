using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class Door : Interactable
{
    [SerializeField] private Transform linkedDoor;

    protected override void Interact()
    {
        base.Interact();
        StartCoroutine(Transport());
    }

    private IEnumerator Transport()
    {
        StartCoroutine(FadeUI.Instance.FadeToBlack());
        yield return new WaitForSeconds(FadeUI.Instance.Duration);
        handler.GetComponent<PlayerController>().TransportPlayer(linkedDoor.position);
        yield return new WaitForSeconds(FadeUI.Instance.Duration*2);
        StartCoroutine(FadeUI.Instance.FadeToBlack(false));
    }
}
