using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingHardWallController : MonoBehaviour
{

    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
