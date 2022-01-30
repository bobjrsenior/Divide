using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float dampener;
    public float jumpStrength;
    public float curHorizontalVelocity;
    private Rigidbody2D rigidbody2D;
    public bool canJump;
    public float xVelocity;
    public float yVelocity;
    public static int collisions;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        collisions = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*moveKeyDown = false;
        //if(rigidbody2D.velocity.x < maxHorizontalSpeed)
        {
            if(Input.GetKey(KeyCode.D))
            {
                rigidbody2D.AddForce(movementSpeed * Time.deltaTime * Vector2.right);
                moveKeyDown = true;
            }
        }
        //if(rigidbody2D.velocity.x > -maxHorizontalSpeed)
        {
            if(Input.GetKey(KeyCode.A))
            {
                rigidbody2D.AddForce(-movementSpeed * Time.deltaTime * Vector2.right);
                moveKeyDown = true;
            }
        }
        //if(!moveKeyDown)
        //    rigidbody2D.velocity -= rigidbody2D.velocity * dampener * Time.deltaTime;
        */
        if(Input.GetAxis("Horizontal") > 0 && xVelocity < movementSpeed)
        {
            //rigidbody2D.velocity.x (movementSpeed * Time.deltaTime * Vector2.right);
            xVelocity += movementSpeed * Input.GetAxis("Horizontal");
            if(xVelocity > movementSpeed)
                xVelocity = movementSpeed;
        }
        if(Input.GetAxis("Horizontal") < 0 && xVelocity > -movementSpeed)
        {
            //rigidbody2D.AddForce(-movementSpeed * Time.deltaTime * Vector2.right);
            xVelocity += movementSpeed * Input.GetAxis("Horizontal");
            if(xVelocity < -movementSpeed)
                xVelocity = -movementSpeed;
        }

        if(Input.GetAxis("Vertical") > 0 && yVelocity < movementSpeed)
        {
            //rigidbody2D.velocity.x (movementSpeed * Time.deltaTime * Vector2.right);
            yVelocity += movementSpeed * Input.GetAxis("Vertical");
            if(yVelocity > movementSpeed)
                yVelocity = movementSpeed;
        }
        if(Input.GetAxis("Vertical") < 0 && yVelocity > -movementSpeed)
        {
            //rigidbody2D.AddForce(-movementSpeed * Time.deltaTime * Vector2.right);
            yVelocity += movementSpeed * Input.GetAxis("Vertical");
            if(yVelocity < -movementSpeed)
                yVelocity = -movementSpeed;
        }
        //if(Input.GetKey(KeyCode.Space) && yVelocity < 0.01)
        //{
        //    //rigidbody2D.AddForce(-movementSpeed * Time.deltaTime * Vector2.right);
        //    yVelocity = jumpStrength;
        //    canJump = false;
        //}
        rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);

        // Dampening
        xVelocity -= xVelocity * dampener * Time.deltaTime;
        yVelocity -= yVelocity * dampener * Time.deltaTime;

        curHorizontalVelocity = rigidbody2D.velocity.x;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        collisions++;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        collisions--;
    }
}
