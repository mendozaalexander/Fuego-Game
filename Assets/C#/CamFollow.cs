using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 velocity;
    public float smoothY;
    public float smoothX;

    public GameObject player;
    public bool bounds;

    public Vector3 minCamPos;
    public Vector3 maxCamPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothY);

        transform.position = new Vector3(posX, posY, transform.position.z);

        if(bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamPos.x, maxCamPos.x),
                Mathf.Clamp(transform.position.y, minCamPos.y, maxCamPos.y),
                Mathf.Clamp(transform.position.z, minCamPos.z, maxCamPos.z));
        }
    }
}
