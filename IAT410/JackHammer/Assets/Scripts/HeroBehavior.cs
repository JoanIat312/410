using UnityEngine;
using System.Collections;

public class HeroBehavior : MonoBehaviour
{
    public GameManager gameManager;
    private Vector3 playerPos;
    private Vector3 playerDirection;
    private bool found;
    public int speed;
    private Rigidbody rb;
    float xDif, yDif;
    // Use this for initialization
    void Start()
    {
        
        found = false;
        speed = 1;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(found == true)
        {
            Debug.Log("found hero");
            playerPos = GameObject.Find("Player").transform.position;
            playerDirection = new Vector3(playerPos.x - transform.position.x + 2, playerPos.y - transform.position.y + 2, playerPos.z);
            transform.Translate(playerDirection *speed * Time.deltaTime);
            //rb.velocity += targetDirection * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            found = true;
        }
    }

}