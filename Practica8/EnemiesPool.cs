using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private GameManager gm;
    [SerializeField] private EnemyMovementScore enem;
    [SerializeField] private float spawnTime = 3f;
    [SerializeField] private float xSpanwPosition = 20f;

    private float timeSinceLastSpawn;
    private int counter;

    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enem.gM = gm; //to assign GameManager to each enemy created in runtime
        enemies = new GameObject[poolSize];
        for(int i=0; i< poolSize; i++)
        {
           enemies[i] = Instantiate(enemyPrefab);
           enemies[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn > spawnTime)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        timeSinceLastSpawn = 0f;
        Vector2 spawnPosition = new Vector2(xSpanwPosition, -3f);
        enemies[counter].transform.position = spawnPosition;

        //to avoid setting active an active enemy
        if (!enemies[counter].activeSelf)
        {
            enemies[counter].SetActive(true);
        }
        counter++;

        if(counter == poolSize)
        {
            counter = 0;
        }
    }
}
