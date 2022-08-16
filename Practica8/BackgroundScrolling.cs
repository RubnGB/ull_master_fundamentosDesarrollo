using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private float spriteWidth;
    private Transform cameraTransform;
    private Rigidbody2D rb;

    public GameManager gM;
    private bool backgroundMoving = false;
    private int scrollSolution;
    private float speed;
    private bool parallax;
    [SerializeField] private float parallaxMultiplier;
    private Vector3 previousCameraPosition;

    void Start()
    {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody2D>();
        scrollSolution = gM.scrollOption;
        speed = gM.speedScrolling;
        parallax = gM.isParallax;
        previousCameraPosition = cameraTransform.position;
        if (scrollSolution == 1 && parallax)
        {
            speed *= (1-parallaxMultiplier);
        }
    }

    // Update is called once per frame
    void Update()
    {
            //when camera position is in the right border of the background
            if (transform.position.x + spriteWidth < cameraTransform.position.x)
            {
                transform.Translate(new Vector3(spriteWidth, 0, 0));
            }
            //when camera position is in the left border of the background
            if (transform.position.x - spriteWidth > cameraTransform.position.x)
            {
                transform.Translate(new Vector3(-spriteWidth, 0, 0));
            }
            
            if (scrollSolution == 1 && !gM.isGameOver)
            {
                backgroundMoving = true;

            }
            else {
                backgroundMoving = false;
            }
            
            if(scrollSolution == 0 && parallax && !gM.isGameOver)
            {
                float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * parallaxMultiplier;
                transform.Translate(new Vector3(deltaX, 0, 0));
                previousCameraPosition = cameraTransform.position;
            }
            
            
            
    }
    private void FixedUpdate()
    {
        if (backgroundMoving && !rb.CompareTag("Ground"))
        {
            rb.velocity = new Vector2(speed * -1, 0);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
