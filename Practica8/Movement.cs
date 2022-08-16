using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameManager gM;
    private int movementMode =0;
    private Rigidbody2D rigid;
    private Animator anim;
    private bool jump;
    private bool isGrounded;
    [SerializeField] private float jumpForce = 6;
    private float horizontalMovement = 0f;
    [SerializeField] private float speedMovement = 150f;
    private bool isFacingLeft = false;


    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movementMode = gM.scrollOption;
    }
    void Update()
    {
        if (movementMode != 1)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal") * speedMovement;
            anim.SetFloat("speed", Mathf.Abs(horizontalMovement));
        }
        if (Input.GetKeyDown(KeyCode.Space)&&isGrounded)
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
        rigid.velocity = new Vector2(horizontalMovement * Time.fixedDeltaTime, rigid.velocity.y);
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gM.GameOver();
            gameObject.SetActive(false);
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
}
