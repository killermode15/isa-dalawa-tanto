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
        if (detectedPlayer)
            StartCoroutine(Transport());
    }

    private IEnumerator Transport()
    {
        StartCoroutine(FadeUI.Instance.FadeToBlack());
        yield return new WaitForSeconds(FadeUI.Instance.Duration);
        level.SetConstraint();

        //stop movement input and velocity of player
        detectedPlayer.GetComponent<CharacterController>().StopMovement();
        detectedPlayer.GetComponent<MovementInput>().Enabled(false);
        //transport player
        detectedPlayer.transform.position = linkedDoor.position;

        yield return new WaitForSeconds(FadeUI.Instance.Duration * 2);
        StartCoroutine(FadeUI.Instance.FadeToBlack(false));
        yield return new WaitForSeconds(FadeUI.Instance.Duration);

        //restore movement input of player
        detectedPlayer.GetComponent<MovementInput>().Enabled(true);
    }
}
