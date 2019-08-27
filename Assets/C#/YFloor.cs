using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YFloor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bridge;
    void Awake()
    {
        bridge = GameObject.FindWithTag("YFloor");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }


}
