using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneLoader : MonoBehaviour
{
    private Animator transitionAnim;
    [SerializeField] private string nameLevel;
    [SerializeField] private GameObject fade;
    private Scene sc2;
    private MainMenu mm;

    private void Start()
    {
        sc2 = SceneManager.GetActiveScene();
        transitionAnim = fade.GetComponent<Animator>();
        mm = GetComponent<MainMenu>();
        if (sc2.name != "Level_1" && sc2.name != "Main_menu")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().jumpingPower = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //when player touch the object's collider which change the scene
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Loader());
        }
    }

    public IEnumerator Loader()
    {
        if (transitionAnim.gameObject.activeSelf) { 
            transitionAnim.SetTrigger("StartTransition");//starts FadeIn Transition
        }
        yield return new WaitForSeconds(1);//wait 1 second
        if (sc2.name == "Main_menu")
        {
            
            if (mm.playB)
            {
                SceneManager.LoadScene("Level_1");
            }
            if (mm.quitB)
            {
                EditorApplication.isPlaying = false;//comment this line when build the game
                Application.Quit();
            }
            if (mm.continueB)
            {
                //to do
            }
        }
        else
        {
            SceneManager.LoadScene(nameLevel);
        }
        
    }
}
