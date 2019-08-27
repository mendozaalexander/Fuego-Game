using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
    bool hasLoadedNewScene = false;
    public GameObject title;

    // Use this for initialization
    void Start()
    {
        title = GameObject.FindWithTag("Title");
    }

    // Update is called once per frame
    
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasLoadedNewScene = true;

           SceneManager.LoadScene("Play");
        }
    }
}