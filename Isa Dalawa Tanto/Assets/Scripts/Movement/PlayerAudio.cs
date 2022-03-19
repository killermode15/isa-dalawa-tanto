using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MovementInput input;
    [SerializeField] private CharacterController controller;
    [SerializeField] private AudioManager audioManager;

    [Space]
    [Header("Volume")]
    [Range(0,1)]
    [SerializeField] private float jumpVolume = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if(controller.Grounded && input.HasJumped)
        {
            AudioSource source = audioManager.GetAudio("player_jump").PlayAudio(destroyAfter: true);
            source.volume = jumpVolume;
        }
    }
}