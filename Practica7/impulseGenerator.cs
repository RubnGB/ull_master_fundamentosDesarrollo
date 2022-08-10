using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class impulseGenerator : MonoBehaviour
{
    public Cinemachine.CinemachineImpulseSource impulseSource;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.F)))
        {
            impulseSource.GenerateImpulse();
        }
    }
}
