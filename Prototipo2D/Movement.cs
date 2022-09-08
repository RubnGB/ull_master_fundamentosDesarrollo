using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public GameManager gM;
    private Scene sc2;
    private Rigidbody2D rigid;
    private Animator anim;
    private bool jump;
    private bool isGrounded;
    [SerializeField] private float jumpForce = 6;
    private float horizontalMovement = 0f;
    [SerializeField] private float speedMovement = 150f;
    [HideInInspector] public bool isFacingLeft = false;
    public bool jumpingPower = false; //the player needs to get the powerUp to jump
    private AudioSource jumpingSound;
    public bool shootingPower = false; //the player needs to get the powerUp to shoot
    private float shotOffset = 1.2f;
    private ShotPool pool;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpingSound = GetComponent<AudioSource>();
        sc2 = SceneManager.GetActiveScene();
        if (sc2.name.Equals("Level_3"))
        {
            pool = GameObject.Find("shotPool").GetComponent<ShotPool>();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpingPower)
        {
            jump = true;
            anim.SetBool("isJumping", true);
            jumpingSound.Play();
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
        Flip();
        if (gM.scrollOption == 0)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal") * speedMovement;
            anim.SetFloat("speed", Mathf.Abs(horizontalMovement));
        }
        //to allow shooting
        if(Input.GetButtonDown("Fire1") && shootingPower)
        {
            anim.SetBool("isShooting", true);
            GameObject shot = pool.RequestShot();
            if (!isFacingLeft)
            {
                //Instantiate(shotPrefab, transform.position + Vector3.right * shotOffset, Quaternion.identity);
                shot.transform.position = transform.position + Vector3.right * shotOffset;
            }
            else
            {
                //Instantiate(shotPrefab, transform.position + Vector3.left * shotOffset, Quaternion.identity);
                shot.transform.position = transform.position + Vector3.left * shotOffset;
            }

        }
        else
        {
            anim.SetBool("isShooting", false);
        }
        
        
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
        if (collision.gameObject.CompareTag("Enemy")||collision.gameObject.CompareTag("Spikes"))
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
