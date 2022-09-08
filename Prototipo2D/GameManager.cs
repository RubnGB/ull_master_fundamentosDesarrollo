using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int scrollOption = 0;//1 camera is static and player only can jump, background is scrolling.
    public float speedScrolling =2.5f;
    public float enemySpeed = 2.5f;
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject pool;
    public bool isParallax = false;
    public bool enemies = false;
    public bool isGameOver;
    private int score;
    private PowerUp pU;
    
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level_1")
        {
            pU = GameObject.FindObjectOfType<PowerUp>().GetComponent<PowerUp>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && isGameOver)
        {
            RestartGame();
        }
        if (scrollOption == 1)
        {
            camera1.SetActive(false);
        }
        if (!enemies)
        {
            pool.SetActive(false);
        }
        else
        {
            pool.SetActive(true);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.SetActive(true);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        if (score == 5)
        {
            pU.stopBattle();
        }
    }

}
