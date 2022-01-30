using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleParent : MonoBehaviour
{
    public GameObject mainCircle;
    public Rigidbody2D nucleusPosition;
    public List<ParticleBanding> childParticles = new List<ParticleBanding>();
    public CircleParent circleParent;

     // Update is called once per frame
    void Update()
    {
        // Decrease Fidelity (only if we are a leaf parent)
        if(Input.GetKeyDown(KeyCode.Q) && childParticles.Count > 0 && PlayerMovement.collisions == 0)
        {
            Reform();            
        }
    }

    public void Reform()
    {
        if(childParticles.Count > 0)
        {
            float subdividerDist = childParticles[0].subdividerDist;
            bool canSubdivide = childParticles[0].canSubdivide;
            // Give up circles
            foreach(ParticleBanding circle in childParticles)
            {
                CircleObjectPool.GiveCircle(circle);
            }

            // Create a new larger circle
            ParticleBanding newCircle = CircleObjectPool.GetCircle();
            
            newCircle.transform.position = transform.position;
            newCircle.transform.parent = transform.parent;
            newCircle.transform.localScale = new Vector2(1.0f, 1.0f);
            newCircle.nucleusPosition = nucleusPosition;
            newCircle.playerMovement = nucleusPosition.GetComponent<PlayerMovement>();
            newCircle.parentParticleForReformation = circleParent;
            newCircle.subdividerDist = subdividerDist;
            newCircle.canSubdivide = canSubdivide;
            if(circleParent != null)
            {
                circleParent.childParticles.Add(newCircle);
                newCircle.spriteRenderer.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }
            else
            {
                newCircle.spriteRenderer.color = Color.white;
            }

            newCircle.SetOffset(nucleusPosition.transform.position - transform.position);
            newCircle.gameObject.SetActive(true);

            Destroy(this.gameObject);
        }
    }
}
