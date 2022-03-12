using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class Door : Interactable
{
    [SerializeField] private LevelCameraConstraint level;
    [SerializeField] private Transform linkedDoor;

    protected override void Interact()
    {
        base.Interact();
        StartCoroutine(Transport());
    }

    private IEnumerator Transport()
    {
        PlayerController controller = handler.GetComponent<PlayerController>();

        StartCoroutine(FadeUI.Instance.FadeToBlack());
        yield return new WaitForSeconds(FadeUI.Instance.Duration);
        level.SetConstraint();
        controller.TransportPlayer(linkedDoor.position);
        controller.SetControllerActive(false);
        yield return new WaitForSeconds(FadeUI.Instance.Duration*2);
        StartCoroutine(FadeUI.Instance.FadeToBlack(false));
        yield return new WaitForSeconds(FadeUI.Instance.Duration);
        controller.SetControllerActive(true);
    }
}
