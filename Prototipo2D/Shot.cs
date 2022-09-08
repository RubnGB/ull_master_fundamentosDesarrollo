using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private float shotSpeed = 5f;
    private Rigidbody2D shotRb;

    private void OnEnable()//to give velocity when shot is active instead of when shot is create
    {
        shotRb = GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().isFacingLeft)
        {
            shotRb.velocity = Vector2.left * shotSpeed;
        }
        else
        {
            shotRb.velocity = Vector2.right * shotSpeed;
        }
    }

    // Update is called once per frame
    private void OnCollisionEnter2D()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
