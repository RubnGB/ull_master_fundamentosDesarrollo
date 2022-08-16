using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int scrollOption = 0;
    public float speedScrolling =2.5f;
    public float enemySpeed = 2.5f;
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private TMP_Text scoreText;
    public bool isParallax = false;
    public bool enemies = false;
    public bool isGameOver;
    private int score;
    
    // Start is called before the first frame update
    void Start()
    {
        if(scrollOption == 1)
        {
            camera1.SetActive(false);
        }
        if (!enemies)
        {
            GameObject.Find("EnemiesPool").SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && isGameOver)
        {
            RestartGame();
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
    }

}
