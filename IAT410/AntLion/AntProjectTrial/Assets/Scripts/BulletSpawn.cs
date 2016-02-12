using UnityEngine;
using System.Collections;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bObject;
    public float fireRate = 0.33f;
    private float timestamp;
    // Use this for initialization
    void Start()
    {
        transform.position = GameObject.Find("jackhammer-gun").transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Update()
    {

       
        if (Input.GetMouseButton(0) && Time.time >= timestamp)
        {
            Debug.Log("left pressed");
            Spawn();


        }
    }

    void Spawn()
    {
        timestamp = Time.time + fireRate;
        GameObject newBullet = Instantiate(bObject, new Vector3(transform.position.x +0.6f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
        //newBullet.transform.position = transform.position;
        //newBullet.tag = "clone";
        Debug.Log("new bullet created");

    }


}