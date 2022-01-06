using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTextScript : MonoBehaviour
{
    public static int healthValue = 3;
    Text health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = " Health: " + healthValue;
    }
}
