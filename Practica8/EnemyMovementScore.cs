using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScore : MonoBehaviour
{
    private Rigidbody2D rbe;
    private float speed;
    private Animator anim;
    public GameManager gM;

    // Start is called before the first frame update
    void Start()
    {
        rbe = GetComponent<Rigidbody2D>();
        speed = gM.enemySpeed;
        anim = GetComponent<Animator>();
        
    }


    private void FixedUpdate()
    {
        if (!gM.isGameOver)
        {
            rbe.velocity = new Vector2(speed * -1, 0);
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

}
