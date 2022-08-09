using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPhysics : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CollisionEnter, mainObject:" + gameObject.name + ", other:" + collision.collider.name);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("CollisionExit, mainObject:" + gameObject.name + ", other:" + collision.collider.name);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TriggerEnter, mainObject:" + gameObject.name + ", other:" + other.name);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("TriggerExit, mainObject:" + gameObject.name + ", other:" + other.name);
    }


    
}
