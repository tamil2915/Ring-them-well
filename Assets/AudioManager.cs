using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] clips;
    public AudioSource player;

    public void PlayBellSound()
    {
        player.clip = clips[0];
        PlayAudio();
    }

    public void PlayAudio()
    {
        if(player.isPlaying)
        {
            player.Stop(); 
        }
        
        player.Play();
    }

    public void PlayGameOverSound()
    {
        player.clip = clips[1];
        PlayAudio();
    }

    public void PlayGameFailedSound()
    {
        player.clip = clips[2];
        PlayAudio();
    }
}
