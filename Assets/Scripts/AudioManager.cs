using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource controlAudios;

    [SerializeField] private AudioClip[] audios;

    void Start()
    {
        controlAudios = GetComponent<AudioSource>();
    }

    public void SelectAudio (int index)
    {
 
        if (!controlAudios.isPlaying)
        {
            controlAudios.PlayOneShot(audios[index]);
        }
    }
}
