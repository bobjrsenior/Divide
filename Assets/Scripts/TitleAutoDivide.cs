using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAutoDivide : MonoBehaviour
{
    private float timer = 2.7f;
    private bool done = false;

    // Update is called once per frame
    void Update()
    {
        if(!done)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                done = true;
                GetComponent<ParticleBanding>().Divide();
            }
        }   
    }
}
