using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class Door : Interactable
{
    [SerializeField] private LevelCameraConstraint level;
    [SerializeField] private Transform linkedDoor;

    protected override void SetDetectionRadius(Vector2 size)
    {
        BoxCollider2D col = this.GetComponent<BoxCollider2D>();
        col.size = size;
    }

    protected override void Interact()
    {
        base.Interact();
        StartCoroutine(Transport());
    }

    private IEnumerator Transport()
    {
        PlayerMovement controller = handler.GetComponent<PlayerMovement>();

        StartCoroutine(FadeUI.Instance.FadeToBlack());
        yield return new WaitForSeconds(FadeUI.Instance.Duration);
        level.SetConstraint();
        controller.TransportPlayer(linkedDoor.position);
        controller.enabled = false;
        yield return new WaitForSeconds(FadeUI.Instance.Duration * 2);
        StartCoroutine(FadeUI.Instance.FadeToBlack(false));
        yield return new WaitForSeconds(FadeUI.Instance.Duration);
        controller.enabled = true;
    }
}
