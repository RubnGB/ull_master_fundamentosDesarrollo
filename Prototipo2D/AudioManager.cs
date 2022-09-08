using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioClip firstClip;
    public AudioClip otherClip;
    public PowerUp pU;
    private AudioSource audioM;
    private bool once = false;//to do the music change once
    private bool oldClip = true;//if initial music background is on
    private bool changeAudio = false;

    

    void Start()
    {
        audioM = gameObject.GetComponent<AudioSource>();
        firstClip = audioM.clip;
        audioM.Play();
    }
    private void Update()
    {
        if(pU.combat)
        {
            changeAudio = true;
        }
        if (once == false)
        {
            
            if (changeAudio)
            {
                audioM.clip = otherClip;
                audioM.Play();
                once = true;
                oldClip = false;
            }
        }
        if(!oldClip && !pU.combat)
        {
            audioM.clip = firstClip;
            audioM.Play();
            oldClip = true;
        }
        
    }
}
