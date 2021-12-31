using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalMove;
    private float verticalMove;
    public CharacterController player;
    private Vector3 playerInput;
    public float playerSpeed = 8.0f; //le asigno una velocidad inicial al personaje pero lo puedo modificar
    private Vector3 directionMovePlayer;
    public float gravity = 9.8f; //es la gravedad que tenemos en La Tierra, podemos modificarlo a nuestro gusto
    private float fallVelocity; //la creamos para que al empezar de nuevo el bucle del Update, no se pierda el valor de la
    //aceleraci�n de la gravedad (porque en playerInput ponemos la Y a 0 cada vez que el bucle pasa por la tercera instrucci�n)
    public float jumpForce; //es el valor que podemos asignar al impulso realizado al saltar

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    // Start is called before the first frame update
    //solo se va a ejecutar al inicializar el script
    void Start()
    {
        player = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    //se ejecuta cada frame por segundo, aqu� ir� la l�gica del juego
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        playerInput = new Vector3(horizontalMove, 0, verticalMove); //si dejamos a 0 el valor intermedio lo que hacemos es impedir el movimiento en el eje y
        playerInput = Vector3.ClampMagnitude(playerInput, 1); /*esto es para limitar que los valores solo puedan ser 0 o 1, ya que en caso de no ponerlo 
                                                               * lo que ocurre es que si nos movemos en diagonal el personaje va m�s r�pido que en movimientos
                                                               * horizontales o verticales*/

        camDirection();//funci�n que me va a permitir conocer hacia d�nde est� mirando la c�mara
        directionMovePlayer = playerInput.x * camRight + playerInput.z * camForward;
        directionMovePlayer = directionMovePlayer * playerSpeed;
        player.transform.LookAt(player.transform.position + directionMovePlayer);
        //estas cuatro l�neas de c�digo junto a player.Move consiguen que el personaje se mueva respecto a la c�mara

        SetGravity(); //aplica la fuerza de la gravedad en cada frame
        PlayerJump(); //funci�n para controlar el salto
        player.Move(directionMovePlayer * Time.deltaTime);
        
    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0; //de momento no voy a controlar el movimiento en el eje Y

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
    //solamente nos interesa para las f�sicas y movimientos
    void SetGravity()
    {
        if (player.isGrounded)//si el personaje est� en el suelo solo aplica gravedad sin aceleraci�n
        {
            fallVelocity = -gravity * Time.deltaTime;
            directionMovePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime; //si el personaje est� en el aire, le resta gravedad a lo que ya hab�a, por tanto lo que hace es acelerar la ca�da
            directionMovePlayer.y = fallVelocity;
        }
        
    }

    void PlayerJump()
    {
        if(player.isGrounded && Input.GetButtonDown("Jump"))//solo saltar� si est� en el suelo y se presiona la tecla de salto
        {
            fallVelocity = jumpForce; //se le da un valor positivo porque vamos hacia arriba en el eje Y (a diferencia de la gravedad,
                                      //que al ir hacia abajo tiene que ser negativo
            directionMovePlayer.y = fallVelocity; //no hay que usar time.deltatime porque no es una aceleraci�n, sino un impulso
        }
    }
 
}
