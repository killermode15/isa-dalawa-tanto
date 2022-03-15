using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : BaseInteractable
{
    [Space()]
    [Header("Door Parameters")]
    [SerializeField] private LevelCameraConstraint level;
    [SerializeField] private Transform linkedDoor;

    public override void OnInteract()
    {
        base.OnInteract();
        StartCoroutine(Transport());
    }

    private IEnumerator Transport()
    {
        //PlayerController controller = handler.GetComponent<PlayerController>();


        StartCoroutine(FadeUI.Instance.FadeToBlack());
        yield return new WaitForSeconds(FadeUI.Instance.Duration);
        level.SetConstraint();
        if (detectedPlayer)
            detectedPlayer.transform.position = linkedDoor.position;

        //NOTE: Disable character movement here
        //controller.TransportPlayer(linkedDoor.position);
        //controller.SetControllerActive(false);
        yield return new WaitForSeconds(FadeUI.Instance.Duration * 2);
        StartCoroutine(FadeUI.Instance.FadeToBlack(false));
        yield return new WaitForSeconds(FadeUI.Instance.Duration);
        //controller.SetControllerActive(true);
    }
}
