using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    private static AudioController controller = null;
    void Awake()
    {
        if(controller != null)
        {
            Destroy(this.gameObject);
        }
        controller = this;

        DontDestroyOnLoad(this.gameObject);

        //transform.position = Camera.main.transform.position;
        //transform.parent = Camera.main.transform;
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}
