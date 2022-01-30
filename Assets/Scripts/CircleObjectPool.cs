using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObjectPool : MonoBehaviour
{
    private static Queue<ParticleBanding> freeCircleObjects = new Queue<ParticleBanding>();
    private static GameObject circlePrefab;
    public GameObject circlePrefabPublic;

    void Awake()
    {
        circlePrefab = circlePrefabPublic;
        freeCircleObjects.Clear();
    }

    public static ParticleBanding GetCircle()
    {
        if(freeCircleObjects.Count > 0){
            return freeCircleObjects.Dequeue();
        }

        ParticleBanding newCircle = Instantiate(circlePrefab).GetComponent<ParticleBanding>();
        newCircle.gameObject.SetActive(false);
        return newCircle;
    }

    public static void GiveCircle(ParticleBanding circle)
    {
        circle.ResetForAdoption();
        freeCircleObjects.Enqueue(circle);
    }
}
