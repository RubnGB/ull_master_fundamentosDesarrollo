using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private float spriteWidth;
    private Transform cameraTransform;
    private Rigidbody2D rb;
    private float speed;
    public GameManager gM;
    private bool backgroundMoving = false;
    [SerializeField] private float parallaxMultiplier;
    private Vector3 previousCameraPosition;

    void Start()
    {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody2D>();
        previousCameraPosition = cameraTransform.position;
        

    }

    // Update is called once per frame
    void Update()
    {
            //when camera position is in the right border of the background, +- 4 is to make the translate quicker
            if (transform.position.x + spriteWidth < cameraTransform.position.x +4)
            {
                transform.Translate(new Vector3(spriteWidth, 0, 0));
            }
            //when camera position is in the left border of the background
            if (transform.position.x - spriteWidth > cameraTransform.position.x -4)
            {
                transform.Translate(new Vector3(-spriteWidth, 0, 0));
            }
            
            if (gM.scrollOption == 1 && !gM.isGameOver)
            {
                backgroundMoving = true;

            }
            else {
                backgroundMoving = false;
            }
            
            if(gM.scrollOption == 0 && gM.isParallax && !gM.isGameOver)
            {
                float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * parallaxMultiplier;
                transform.Translate(new Vector3(deltaX, 0, 0));
                previousCameraPosition = cameraTransform.position;
            }

            if (gM.isParallax)
            {
                speed = gM.speedScrolling * (1 - parallaxMultiplier);
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
