using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float leftLimit = 0.3f;
    [SerializeField] private float rightLimit = 0.3f;
    [SerializeField] private float speed;
    private bool once=true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.bodyType == RigidbodyType2D.Dynamic && once)
        {
            rb.angularVelocity = 500;
            once = false;
            
        }
        pendulumMove();
    }

    private void pendulumMove()
    {
        if(transform.rotation.z < rightLimit && rb.angularVelocity > 0 && rb.angularVelocity < speed)
        {
            rb.angularVelocity = speed;
        }else if(transform.rotation.z > leftLimit && rb.angularVelocity < 0 && rb.angularVelocity > -speed)
        {
            rb.angularVelocity = -speed;
        }
    }
}
