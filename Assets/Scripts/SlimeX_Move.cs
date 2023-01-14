using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeX_Move : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Vector3 direccion;
    public Vector3 newDireccion;
    public Rigidbody rigidbody;
    public bool isColliding;
    //public float groundCheckRadius;
    //public GameObject Ground;
    //private LayerMask groundLayer;
    //private bool onGround;
    
    public float delayTime = 5.0f; // the delay time in seconds
    private float timer = 0.0f; // a timer to count up to the delay time

    public float minX = -9.0f; // the minimum x coordinate of the bounding box
    public float maxX = 9.0f; // the maximum x coordinate of the bounding box
    public float minZ = -9.0f; // the minimum y coordinate of the bounding box
    public float maxZ = 9.0f; // the maximum y coordinate of the bounding box

    public Vector3 movement;

    void Start()
    {
        moveSpeed = 1.0f;
        jumpForce = 10.0f;
        
        rigidbody = GetComponent<Rigidbody>();
        Vector3 init = new Vector3(1.0f,0.0f,1.0f);
        direccion = transform.TransformDirection(init.normalized);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, -0.5f, 0.5f),
            Mathf.Clamp(transform.position.z, minZ, maxZ)
        );

        if(isColliding)
        {
            rigidbody.AddForce(newDireccion.normalized * moveSpeed, ForceMode.Impulse);
        }else
        {
            rigidbody.AddForce(direccion.normalized * moveSpeed, ForceMode.Impulse);
        }

        //onGround = Physics.CheckSphere(Ground.transform.position,groundCheckRadius,groundLayer);
        //Debug.Log(onGround);

        //if(Input.GetButtonDown("Jump") )

        //Esta parte se va a usar para arrancar al slime

        //timer += Time.deltaTime;

        //if(timer >= delayTime)
        //{
        //    timer =0.0f;
        //    GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //}

    }

    void OnCollisionEnter(Collision collision)
    {
        // Get the first contact point
        if (collision.gameObject.tag == "Wall")       
         {
            //There's an actual colission            
            isColliding = true;

            ContactPoint contact = collision.contacts[0];
    
            // Calculate the collision point
            Vector3 collisionPoint = contact.point; /* some point in space where the collision occurred */

            Vector3 currentDirection = this.direccion; /* the current direction of the object */

            newDireccion = Vector3.Reflect(currentDirection, collisionPoint);

            Debug.Log("Colliding? :"+isColliding);
        }
    }

        void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Wall")
            {
                isColliding = false;
            }
        }
}   
