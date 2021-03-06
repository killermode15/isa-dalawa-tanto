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
        Transport();
    }

    public override void OnInteractEnd()
    {
        base.OnInteractEnd();
    }

    public void Transport()
    {
        if (detectedPlayer)
            StartCoroutine(Transport_CR());
    }

    private IEnumerator Transport_CR()
    {
        GameObject player = detectedPlayer;

        StartCoroutine(FadeUI.Instance.FadeToBlack(true));
        yield return new WaitForSeconds(FadeUI.Instance.Duration);

        //stop movement input and velocity of player
        player.GetComponent<CharacterController>().StopMovement();
        player.GetComponent<MovementInput>().Enabled(false);
        //transport player
        player.GetComponent<MovementInput>().Enabled(true);
        player.transform.position = linkedDoor.position;

        OnInteractEnd();

        level.SetConstraint();

        yield return new WaitForSeconds(FadeUI.Instance.Duration * 2);
        StartCoroutine(FadeUI.Instance.FadeToBlack(false));
        yield return new WaitForSeconds(FadeUI.Instance.Duration);

        //restore movement input of player
    }
}
