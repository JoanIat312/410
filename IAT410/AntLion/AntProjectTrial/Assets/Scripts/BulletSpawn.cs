using UnityEngine;
using System.Collections;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bObject;
    private Vector3 newXPos;
    public float fireRate = 0.33f;
    private float timestamp;
    // Use this for initialization
    void Start()
    {
        transform.position = GameObject.Find("Ant_Player").transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Update()
    {

        //newXPos++;
        //secondsBetweenSpawn -= Random.Range (0.001f, 0.08f);
        //newXPos = Mathf.PingPong (Time.time * moveSpeed, moveDistance) - (moveDistance/2f);
        if (Input.GetMouseButton(0) && Time.time >= timestamp)
        {
            Debug.Log("left pressed");
            Spawn();


        }
    }

    void Spawn()
    {
        timestamp = Time.time + fireRate;
        GameObject newBullet = Instantiate(bObject, transform.position, transform.rotation) as GameObject;
        newBullet.transform.position = transform.position;
        newBullet.tag = "clone";
        Debug.Log("new bullet created");

    }


}