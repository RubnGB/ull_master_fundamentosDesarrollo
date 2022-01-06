using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerTextValue : MonoBehaviour
{
    Text power;
    // Start is called before the first frame update
    void Start()
    {
        power = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        power.text = " Power: " + PushRigidBody.powerValue;
    }
}
