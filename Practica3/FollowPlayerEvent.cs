using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerEvent : MonoBehaviour
{
    //public GameObject enemigo;
    public Transform player;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movement;
    private float magnit;
    public float limit = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {

        Vector3 direction = player.position - transform.position;
        magnit = direction.magnitude;
        direction.Normalize();
        movement = direction;
           
    }
    private void FixedUpdate()
    {
        transform.LookAt(player.transform.position + movement);
        if (magnit > limit)
        {
            moveCharacter(movement);
        }
        
    }
    void moveCharacter(Vector3 direction)
    {
        rb.MovePosition(transform.position + (movement * moveSpeed * Time.deltaTime));
    }
}
