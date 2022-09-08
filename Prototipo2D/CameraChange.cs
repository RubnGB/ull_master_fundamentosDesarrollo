using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;
    [SerializeField] private GameObject camera3;
    private bool enable = true;
    private Rigidbody2D rbCrate;
    private Rigidbody2D rbLiana;
    private bool followCrate = false;
    private bool followLiana = false;

    void Start()
    {
        camera1.SetActive(enable);
        camera2.SetActive(!enable);
        camera3.SetActive(!enable);
        rbCrate = GameObject.Find("crate1").GetComponent<Rigidbody2D>();
        rbLiana = GameObject.Find("liana").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rbCrate.bodyType == RigidbodyType2D.Dynamic && !followCrate)
        {
            camera1.SetActive(!enable);
            camera2.SetActive(enable);
            camera3.SetActive(!enable);
            StartCoroutine(WaitingCamera());
            followCrate = true; //to do the change once
        }
        else if (rbLiana.bodyType == RigidbodyType2D.Dynamic && !followLiana)
        {
            camera1.SetActive(!enable);
            camera2.SetActive(!enable);
            camera3.SetActive(enable);
            StartCoroutine(WaitingCamera());
            followLiana = true; //to do the change once
        }

    }

    private IEnumerator WaitingCamera()
    {
        yield return new WaitForSecondsRealtime(3);
        camera1.SetActive(enable);//camera that follows the player
        camera2.SetActive(!enable);
        camera3.SetActive(!enable); 
    }
}
