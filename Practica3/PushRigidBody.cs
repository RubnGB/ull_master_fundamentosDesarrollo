using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushRigidBody : MonoBehaviour
{
    
    public static float powerValue = 5; // fuerza con la que vamos a empujar el objeto
    public int scoreIncreasementValue = 10;
    public float scaleDecreasementValue = 0.3f;
    private bool yellowKey = false; //son las bolas de colores, las tengo que coger para poder mover los objetos
    private bool redKey = false;
    private bool blueKey = false;

    public CharacterController player;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnControllerColliderHit(ControllerColliderHit hit) //solo se activará cuando nuestro player colisione con otro objeto
    {
        Rigidbody body = hit.collider.attachedRigidbody; //cuando colisionemos con un colider, detectaremos el RigidBody asociado y lo almacenamos
        if (body == null || body.isKinematic)
        {
            return; //si el collider con el que chocamos no tiene asociado un Rigidbody no hacemos nada, simplemente salimos de la función o bien si
            //chocamos con un objeto que hemos dicho que es kinematic y que por tanto no se va a mover
        }
        if (hit.moveDirection.y < -0.3) //si caemos encima de un objeto tampoco queremos que se mueva, solo queremos quedarnos encima
        {
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z); //aquí almacenamos la dirección en la que estamos empujando la caja

        //body.velocity = pushDir * powerValue;
        //Color colorObject = body.GetComponent<Renderer>().material.color;

        if (body.gameObject.name == "YellowSphere")
        {
            yellowKey = true;
            Destroy(body.gameObject);
            
        }else if(body.gameObject.name == "BlueSphere")
        {
            blueKey = true;
            Destroy(body.gameObject);
        }else if (body.gameObject.name == "RedSphere")
        {
            redKey = true;
            Destroy(body.gameObject);
        }

        if(body.gameObject.name == "YellowCube" && yellowKey)
        {
            body.velocity = pushDir * powerValue;
            increaseScore();
        }else if(body.gameObject.name == "RedCube" && redKey)
        {
            body.velocity = pushDir * powerValue;
            increaseScore();
        }else if(body.gameObject.name == "BlueCube" && blueKey)
        {
            body.velocity = pushDir * powerValue;
            increaseScore();
        }

        if(body.gameObject.name == "RedPortal")
        {
            player.transform.position = new Vector3(111.69f, 0.62f, 194.51f);
        }else if(body.gameObject.name == "BluePortal")
        {
            player.transform.position = new Vector3(104.69f, 0.62f, 194.51f);
        }
        /*
        if (body.transform.localScale.x > 0.7f)//me serviría cualquiera de las tres variables, ya que en este caso disminuyo uniformemente las tres variables
        {
            if (body.gameObject.name == "YellowCube")
            {
                body.transform.localScale -= new Vector3(scaleDecreasementValue, scaleDecreasementValue, scaleDecreasementValue);
            }
            if(body.gameObject.name!= "Cylinder")
            {
                body.GetComponent<Renderer>().material.color = new Color(colorObject.r * 0.9f, colorObject.g * 0.9f, colorObject.b * 0.9f);
                //Debug.Log(body.gameObject.name);
            }


        }
        else
        {
            Destroy(body.gameObject);//destruye el GameObject con el que está colisionando
        }
        */

        
    }
    void increaseScore()
    {
        ScoreTextScript.scoreValue += scoreIncreasementValue;
        if (ScoreTextScript.scoreValue % 100 == 0)
        {
            powerValue++;
        }
    }
}
