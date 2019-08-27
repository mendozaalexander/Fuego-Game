using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject gem;
    public GameObject bridge;

    // Start is called before the first frame update
    void Start()
    {
        gem = GameObject.FindWithTag("Gem");
        bridge = GameObject.FindWithTag("YFloor");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            //Destroy(bridge);
            Destroy(gem);
        }
    }
}