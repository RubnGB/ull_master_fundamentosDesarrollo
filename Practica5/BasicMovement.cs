using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float jumpForce = 6;
    public float gravity = -9.81f;
    public float gravityScale = 3;//higher value makes fall down faster
    private float horizontal;
    private float velocity;
    public float speed = 7;//horizontal speed
    public float distanceToCheck = 0.14f;//to avoid the character falls inside the floor
    public bool isGrounded;
    private bool isFacingLeft = true;//to check if the character is looking towards left

    public Animator animatorSlime;//blueSlime
    public Animator animator;//redSlime
    public float distance;
    public GameObject secondaryObject;//redSlime

    // Update is called once per frame
    void Update()
    {
        //when press X, the slime red animation changes to hurt
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetBool("isXPressed", true);
        }
        else
        {
           animator.SetBool("isXPressed", false);
        }
        //this raycast check the distance between the character and the floor
        if (Physics2D.Raycast(transform.position, Vector2.down, distanceToCheck))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        velocity += gravity * gravityScale * Time.deltaTime;
        horizontal = Input.GetAxis("Horizontal");
        //this conditional makes the slime stand on the floor and stop falling down
        if (isGrounded && velocity < 0)
        {
            velocity = 0;
        }
        //this conditional allows jumping when the space is pressed and the character is on the floor to avoid multiple jumping in the air
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity = jumpForce;
        }
        //movement horizontal and vertical
        transform.Translate(new Vector2(horizontal*speed, velocity) * Time.deltaTime);
        Flip();
        //change the blue slime animation from idle to moving
        animatorSlime.SetFloat("speed", Mathf.Abs(horizontal * speed));
        //change the red slime animation from idle to death
        distance = Mathf.Abs(transform.position.x - secondaryObject.transform.position.x);
        animator.SetFloat("isNear", distance);
    }
    //flip changes the scale orientation in the X axis
    private void Flip()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        //if we would like flipping only when turn left or right: if (isFacingLeft && horizontal > 0f || !isFacingLeft && horizontal < 0f)
        {
            //isFacingLeft = !isFacingLeft; it is necessary only when the commented conditional
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
