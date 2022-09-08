using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitch : MonoBehaviour
{
    private Animator anim;
    private bool isPlayerInRange;
    [SerializeField] GameObject targetObject;
    private bool once = true; //to control the action happens once
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Switch"))
        {
            if (isPlayerInRange && Input.GetButtonDown("Fire1") && once)
            {
                anim.SetTrigger("isActive");
                if(gameObject.name == "crank")
                {
                    GameObject.Find("platform1").transform.Translate(0, 4.5f, 0);
                    GameObject.Find("platform2").transform.Translate(0, 4.5f, 0);
                    GameObject.Find("platform3").transform.Translate(0, 4.5f, 0);
                }
                targetObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                once = false;
            }
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
