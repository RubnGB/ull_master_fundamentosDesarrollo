using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioS;
    [HideInInspector] public bool playB = false;
    [HideInInspector] public bool continueB = false;
    [HideInInspector] public bool quitB = false;
    private SceneLoader sc;

    private void Start()
    {
        sc = GetComponent<SceneLoader>();
    }

    public void startPlay()
    {
        audioS.Play();
        playB = true;
        StartCoroutine(sc.Loader());        
    }
    public void quit()
    {
        audioS.Play();
        quitB = true;
        StartCoroutine(sc.Loader());
    }
    public void continuePlay()
    {
        audioS.Play();
        continueB = true;
        //StartCoroutine(sc.Loader());
    }
}
