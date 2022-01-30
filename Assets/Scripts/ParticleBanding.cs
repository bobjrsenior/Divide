using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleBanding : MonoBehaviour
{
    public Rigidbody2D nucleusPosition;
    public Vector2 expectedNucleusOffset;
    public PlayerMovement playerMovement;
    public float bandingMultiplier;
    public CircleParent parentParticleForReformation;
    public GameObject parentParticlePrefab;
    private Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;
    public float lossyScaleX;
    public bool canSubdivide;
    public float subdividerDist = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        InitOffset();
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void InitOffset()
    {
        if(nucleusPosition != null)
            expectedNucleusOffset = nucleusPosition.transform.position - transform.position;
    }

    public void SetOffset(Vector2 offset)
    {
        expectedNucleusOffset = offset;
    }

    public void ResetForAdoption()
    {
        gameObject.SetActive(false);
        transform.SetParent(null);
        rigidbody2D.velocity = Vector2.zero;
        playerMovement = null;
        nucleusPosition = null;
        parentParticleForReformation = null;
    }

    void Update()
    {
        // Increase Fidelity
        if(Input.GetKeyDown(KeyCode.E) && canSubdivide && transform.lossyScale.x > 0.0630) // Rounded up for floating point innaccuracy
        {
            Divide();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(nucleusPosition != null)
        {
            Vector2 curOffset = nucleusPosition.transform.position - transform.position;
            Vector2 diff = curOffset - expectedNucleusOffset;
            Vector2 normalized = diff.normalized;
            float mag = diff.magnitude;
            float sqrMag = diff.sqrMagnitude;
            float force = mag * bandingMultiplier;
            rigidbody2D.velocity = (force * normalized);
            if(playerMovement != null)
            {
                playerMovement.xVelocity -= force / 3.5f * normalized.x * transform.lossyScale.x * transform.lossyScale.x;
                playerMovement.yVelocity -= force / 3.5f * normalized.y * transform.lossyScale.y * transform.lossyScale.y;
            }
            //nucleusPosition.AddForce((-force / 2.0f) * normalized);
        }
        lossyScaleX = transform.lossyScale.x;
    }

    public void Divide()
    {
        float lossyScale = transform.lossyScale.x;
        float newLossyScale = lossyScale / 2;
        //float offsetAmountDiagnal = 0.708f * newLossyScale; // Only if donig a tringle divide
        float offsetAmount = subdividerDist * newLossyScale;

        // Create empty parent
        GameObject newParent = Instantiate(parentParticlePrefab, ((Vector2)nucleusPosition.transform.position) + expectedNucleusOffset, Quaternion.identity) as GameObject;
        CircleParent circleParent = newParent.GetComponent<CircleParent>();
        newParent.transform.parent = transform.parent;
        circleParent.transform.localScale = new Vector2(0.5f, 0.5f);
        circleParent.nucleusPosition = nucleusPosition;
        circleParent.mainCircle = this.gameObject;
        // Disable self
        gameObject.SetActive(false);
        // Create new children
        ParticleBanding circle1 = CircleObjectPool.GetCircle();
        ParticleBanding circle2 = CircleObjectPool.GetCircle();
        ParticleBanding circle3 = CircleObjectPool.GetCircle();
        ParticleBanding circle4 = CircleObjectPool.GetCircle();

        // Color
        circle1.spriteRenderer.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        circle2.spriteRenderer.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        circle3.spriteRenderer.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        circle4.spriteRenderer.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));


        // Children Position
        circle1.transform.position = new Vector2(transform.position.x - offsetAmount, transform.position.y - offsetAmount);
        circle2.transform.position = new Vector2(transform.position.x + offsetAmount, transform.position.y - offsetAmount);
        circle3.transform.position = new Vector2(transform.position.x - offsetAmount, transform.position.y + offsetAmount);
        circle4.transform.position = new Vector2(transform.position.x + offsetAmount, transform.position.y + offsetAmount);

        // Children Parent
        circle1.transform.SetParent(newParent.transform);
        circle2.transform.SetParent(newParent.transform);
        circle3.transform.SetParent(newParent.transform);
        circle4.transform.SetParent(newParent.transform);

        // Children Scale
        circle1.transform.localScale = new Vector2(1.0f, 1.0f);
        circle2.transform.localScale = new Vector2(1.0f, 1.0f);
        circle3.transform.localScale = new Vector2(1.0f, 1.0f);
        circle4.transform.localScale = new Vector2(1.0f, 1.0f);

        // Children Script setup
        circle1.playerMovement = this.playerMovement;
        circle2.playerMovement = this.playerMovement;
        circle3.playerMovement = this.playerMovement;
        circle4.playerMovement = this.playerMovement;

        circle1.subdividerDist = this.subdividerDist;
        circle2.subdividerDist = this.subdividerDist;
        circle3.subdividerDist = this.subdividerDist;
        circle4.subdividerDist = this.subdividerDist;

        circle1.canSubdivide = this.canSubdivide;
        circle2.canSubdivide = this.canSubdivide;
        circle3.canSubdivide = this.canSubdivide;
        circle4.canSubdivide = this.canSubdivide;

        circle1.nucleusPosition = this.nucleusPosition;
        circle2.nucleusPosition = this.nucleusPosition;
        circle3.nucleusPosition = this.nucleusPosition;
        circle4.nucleusPosition = this.nucleusPosition;

        circle1.parentParticleForReformation = circleParent;
        circle2.parentParticleForReformation = circleParent;
        circle3.parentParticleForReformation = circleParent;
        circle4.parentParticleForReformation = circleParent;

        circle1.SetOffset(new Vector2(expectedNucleusOffset.x + offsetAmount, expectedNucleusOffset.y + offsetAmount));
        circle2.SetOffset(new Vector2(expectedNucleusOffset.x - offsetAmount, expectedNucleusOffset.y + offsetAmount));
        circle3.SetOffset(new Vector2(expectedNucleusOffset.x + offsetAmount, expectedNucleusOffset.y - offsetAmount));
        circle4.SetOffset(new Vector2(expectedNucleusOffset.x - offsetAmount, expectedNucleusOffset.y - offsetAmount));

        if(parentParticleForReformation != null)
        {
            circleParent.circleParent = parentParticleForReformation;
            circleParent.circleParent.childParticles.Clear();
        }
        circleParent.childParticles.Add(circle1);
        circleParent.childParticles.Add(circle2);
        circleParent.childParticles.Add(circle3);
        circleParent.childParticles.Add(circle4);

        // Finally set active
        circle1.gameObject.SetActive(true);
        circle2.gameObject.SetActive(true);
        circle3.gameObject.SetActive(true);
        circle4.gameObject.SetActive(true);

        // Give this circle up for adoption
        CircleObjectPool.GiveCircle(this);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(parentParticleForReformation == null && col.tag == "Goal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(playerMovement != null && col.tag == "Spike")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
