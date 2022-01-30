using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongHardWallController : MonoBehaviour
{

    public float movementSpeed;
    public Vector2 distance;
    private Vector2 distanceNorm;
    private Vector2 dest;
    private Vector2 origPos;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        distanceNorm = distance.normalized;
        origPos = transform.position;
        dest = ((Vector2)transform.position) + distance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody2D.velocity = distanceNorm * movementSpeed;
        if(Mathf.Abs((((Vector2)transform.position) - dest).sqrMagnitude) < 0.01f)
        {
            distanceNorm = -distanceNorm;
            Vector2 temp = dest;
            dest = origPos;
            origPos = temp;
        }
    }
}
