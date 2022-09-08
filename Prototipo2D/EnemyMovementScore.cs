using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScore : MonoBehaviour
{
    private Rigidbody2D rbe;
    private float speed;
    private Animator anim;
    public GameManager gM;
    [SerializeField] private int lives = 3;
    private GameObject closedGate;
    private int changeMovementDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        rbe = GetComponent<Rigidbody2D>();
        speed = gM.enemySpeed;
        anim = GetComponent<Animator>();
        closedGate = GameObject.Find("gate");
        
    }


    private void FixedUpdate()
    {
        if (!gM.isGameOver)
        {
            if (gameObject.name.Equals("Octopus"))
            {

                rbe.velocity = new Vector2(0, speed * 1 * changeMovementDirection);
                
            }
            else
            {
                rbe.velocity = new Vector2(speed * -1, 0);
            }
            
        }
        else
        {
            rbe.velocity = Vector2.zero;
            anim.enabled = false;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            gM.IncreaseScore();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("shot")|| collision.gameObject.name.Equals("shot(Clone)"))
        {
            lives--;
            StartCoroutine(enemyHurt());
            //animacion que cambie su color a rojo momentáneamente cada vez que recibe un disparo
            if (lives == 0)
            {
                Destroy(gameObject);
                closedGate.GetComponent<Animator>().SetTrigger("openGate");
                closedGate.GetComponentInChildren<CapsuleCollider2D>().enabled = false;
            }
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            changeMovementDirection = -changeMovementDirection;
        }
    }

    private IEnumerator enemyHurt()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white; //or any default
    }
}
