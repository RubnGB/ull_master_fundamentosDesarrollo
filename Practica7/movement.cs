using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    
    private Rigidbody2D rigid;
    private Animator anim;
    private bool jump;
    private bool isGrounded;
    [SerializeField] private float jumpForce = 6;
    private float horizontalMovement = 0f;
    [SerializeField] private float speedMovement = 150f;
    private bool isFacingLeft = false;
    [SerializeField] private Cinemachine.CinemachineImpulseSource impulseSource;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //horizontalMovement = Input.GetAxisRaw("Horizontal") * speedMovement;
        awsdMovement();
        anim.SetFloat("speed", Mathf.Abs(horizontalMovement));
        //if (Input.GetKeyDown(KeyCode.Space)&&isGrounded)
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            jump = true;
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
        Flip();
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(horizontalMovement * Time.deltaTime, rigid.velocity.y);
        //this conditional allows jumping only when the space is pressed and the character is on the floor to avoid multiple jumping in the air
        if (jump)
        {
            //rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            jump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //to jump only when the player is over a floor where it has sense jump
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void Flip()
    {
        if (isFacingLeft && horizontalMovement > 0f || !isFacingLeft && horizontalMovement < 0f)
        {
            isFacingLeft = !isFacingLeft;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void awsdMovement()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            horizontalMovement = -1 * speedMovement;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            horizontalMovement = 1 *speedMovement;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            horizontalMovement = 0;
        }
    }
}
