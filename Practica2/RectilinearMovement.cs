using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectilinearMovement : MonoBehaviour
{
    
    [SerializeField]
    private float speed=1;
    private Rigidbody rb;
    private int steps=40;
    private int count = 0;

    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }
    void FixedUpdate()
    {
        
        //Debug.Log("ValorAntesDelIf:" + count);
        if (count < steps)
        {
            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
            count++;
        }
        else if(count == steps)
        {
            
            transform.Rotate(0, 180, 0);
            count = 0;
            rb.velocity = Vector3.zero;
            //Debug.Log("ValorDespuésDelElse:"+count);
        }        

    }
}

