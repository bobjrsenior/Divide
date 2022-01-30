using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeOut : MonoBehaviour
{
    private Text text;
    public float fadeTime;
    public float delay;
    private float timer1;
    private bool done;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        timer1 = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if(!done)
        {
            timer1 -= Time.deltaTime;
            if(timer1 < 0)
            {
                Color color = text.color;
                text.color = new Color(color.r, color.g, color.b, color.a - (Time.deltaTime / fadeTime));
                if(text.color.a <= 0.01f)
                {
                    done =true;
                }
            }
        }
    }
}
