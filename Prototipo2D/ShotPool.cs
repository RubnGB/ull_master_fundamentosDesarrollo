using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPool : MonoBehaviour
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private int poolSize = 10;
    private List<GameObject> shotList;

    // Start is called before the first frame update
    void Start()
    {
        shotList = new List<GameObject>();
        AddShotsToPool(poolSize);
    }

    private void AddShotsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject shot = Instantiate(shotPrefab);
            shot.SetActive(false);
            shotList.Add(shot);
            shot.transform.parent = transform; //this make all the objects children of shotPool
        }
    }
    
    public GameObject RequestShot()
    {
        for(int i = 0; i< shotList.Count; i++)
        {
            if (!shotList[i].activeSelf)
            {
                shotList[i].SetActive(true);
                return shotList[i];
            }
        }
        //if you need more bullets than the number of poolSize you can add more with this code
        AddShotsToPool(1);
        shotList[shotList.Count - 1].SetActive(true);
        return shotList[shotList.Count - 1];
    }
   

}
