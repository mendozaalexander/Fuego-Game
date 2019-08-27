using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Control : MonoBehaviour
{
    public float speed = 40f;
    float horizontalMove = 0f;
    public GameObject bridge;
    public GameObject map;
    bool jump = false;
    bool inAir = false;
    bool hit = false;
    bool win = false;
    private float jumpForce = 750;
    private float smoothing = .05f;
    private bool ground = true;
    private Rigidbody2D rBody;
    private bool right = false;
    private Vector3 pVelocity = Vector3.zero;
    Animator anim;
    bool hasLoadedNewScene = false;


    public void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        bridge = GameObject.FindWithTag("YFloor");
        map = GameObject.FindWithTag("Floor");
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        anim.SetBool("grounded",ground);
        anim.SetFloat("pSpeed", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hit && !hasLoadedNewScene)
        {
            hasLoadedNewScene = true;
            //SceneManager.UnloadSceneAsync("Play");
            // SceneManager.LoadScene("Lose");
            StartCoroutine(WaitL());
        }

        if (win && !hasLoadedNewScene)
        {
            hasLoadedNewScene = true;
            // SceneManager.UnloadSceneAsync("Play");
            //SceneManager.LoadScene("Win");
            StartCoroutine(WaitW());
        }

        Vector3 easeVelocity = rBody.velocity;
        easeVelocity.y = rBody.velocity.y;
        easeVelocity.z = 0f;
        easeVelocity.x *= 0.75f;

        if(ground)
        {
            rBody.velocity = easeVelocity;
        }

       
            move(horizontalMove, jump, ground);
        
        if (Input.GetButtonDown("Jump"))
        {
            if (ground)
            {
                inAir = true;
                ground = false;            
                rBody.AddForce(new Vector2(horizontalMove, jumpForce));
            }
        }
        
        
    }

    private void move(float moving, bool jumping, bool isGround)
    {
        if (ground || inAir)
        {
            Vector3 move = new Vector2(moving * 20f, rBody.velocity.y);
            rBody.velocity = Vector3.SmoothDamp(rBody.velocity, move, ref pVelocity, smoothing);           

            if (moving > 0 && !right)
            {
                Flip();
            }
            else if (moving < 0 && right)
            {
                Flip();
            }
            
        }
    }

    private void Flip()
    {
        right = !right;
        Vector3 side = transform.localScale;
        side.x *= -1;
        transform.localScale = side;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Floor"))
        {
            ground = true;
        }
        if (coll.gameObject.CompareTag("YFloor"))
        {
            ground = true;
        }
        if (coll.gameObject.CompareTag("Gem"))
        {
            Destroy(coll.gameObject);
            Destroy(bridge);
        }
        if (coll.gameObject.CompareTag("Kid"))
        {
            Destroy(rBody);
            win = true;
        }
        if (coll.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool("dead", true);
            Destroy(rBody);
            hit = true;
        }
        if (coll.gameObject.name == "Floor")
        {
            ground = true;
        }
        if (coll.gameObject.name == "map")
        {
            ground = true;
        }
    }
    IEnumerator WaitL()
    {
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.UnloadSceneAsync("Play");
        SceneManager.LoadScene("Lose");
    }

    IEnumerator WaitW()
    {
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.UnloadSceneAsync("Play");
        SceneManager.LoadScene("Win");
    }
}
