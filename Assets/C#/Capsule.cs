using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capsule : MonoBehaviour
{
    public string level;
    public Image black;
    public Animator anim;
    bool winT = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (winT)
            StartCoroutine(Fade());
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            winT = true;
        }
    }

    IEnumerator Fade()
    {
        anim.SetBool("fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
    }
}
