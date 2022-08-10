using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changingCamera : MonoBehaviour
{
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;
    [SerializeField] private GameObject cameragroup;
    private bool enabled = true;
    void Start()
    {
        camera1.SetActive(enabled);
        camera2.SetActive(!enabled);
        cameragroup.SetActive(!enabled);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            camera1.SetActive(enabled);
            camera2.SetActive(!enabled);
            cameragroup.SetActive(!enabled);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            camera1.SetActive(!enabled);
            camera2.SetActive(enabled);
            cameragroup.SetActive(!enabled);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            camera1.SetActive(!enabled);
            camera2.SetActive(!enabled);
            cameragroup.SetActive(enabled);
        }
    }
}
