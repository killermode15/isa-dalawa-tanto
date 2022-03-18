using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;


    public void Play(string id)
    {
        audioManager.GetAudio(id).PlayAudio(destroyAfter: true);
    }
}
