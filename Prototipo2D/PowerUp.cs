using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerUp : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spr;
    public bool combat = false;
    public bool dialogSignal = false;
    [SerializeField] private GameObject canvasScore;
    private Scene sc2;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        sc2 = SceneManager.GetActiveScene();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("isActive");
            if (sc2.name == "Level_1")
            {
                StartCoroutine(startBattle());
            }
            else
            {
                //to allow show the animation before dissapear the powerUp icon
                StartCoroutine(waitingTimeBeforeDissapear());
            }              
        }
    }

    private IEnumerator startBattle()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        spr.enabled = false;//the powerUp icon dissapear when the combat starts
        dialogSignal = true;
        combat = true;//active music combat in AudioManager
        GameObject.FindObjectOfType<GameManager>().enemies = true;
        GameObject.FindObjectOfType<GameManager>().scrollOption = 1;
        canvasScore.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().jumpingPower = true;
    }
    public void stopBattle()
    {
        combat = false;
        GameObject.FindObjectOfType<GameManager>().enemies = false;
        GameObject.FindObjectOfType<GameManager>().scrollOption = 0;
        canvasScore.SetActive(false);
        while (GameObject.FindGameObjectWithTag("Enemy"))//disable each enemy of the scene
        {
            GameObject.FindGameObjectWithTag("Enemy").SetActive(false);
        }
    }

    private IEnumerator waitingTimeBeforeDissapear()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        spr.enabled = false;//the powerUp icon dissapear when the combat starts
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().shootingPower = true;
    }
}
