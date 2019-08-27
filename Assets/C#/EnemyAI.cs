using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D enemy;
    public GameObject map;
    bool jump = false;
    private bool ground = true;
    public string level;
    public Image black;
    public Animator anim;
    bool hitD = false;
    // Start is called before the first frame update
    public void Awake()
    {
        enemy = GetComponent<Rigidbody2D>();
        map = GameObject.FindWithTag("Floor");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ground)
        {
            ground = false;
            enemy.AddForce(new Vector2(0, 500));
        }
        if(hitD)
        {
            StartCoroutine(Fade());
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Floor"))
        {
            ground = true;
        }
        if (coll.gameObject.name == "Floor")
        {
            ground = true;
        }
        if (coll.gameObject.CompareTag("Player"))
        {
            hitD = true;
        }
    }

    IEnumerator Fade()
    {
        anim.SetBool("fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
    }
}
