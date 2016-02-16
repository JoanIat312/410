using UnityEngine;
using System.Collections;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bObject;
    public float fireRate;
    private float timestamp;
    // Use this for initialization
    void Start()
    {
        transform.position = GameObject.Find("jackhammer-gun").transform.position;
		fireRate = .5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Update()
    {

       
		if ((Input.GetMouseButton(0)) && (Time.time >= timestamp))
        {
            Debug.Log("left pressed");
            Spawn();


        }
    }

    void Spawn()
    {
        timestamp = Time.time + fireRate;
        GameObject newBullet = Instantiate(bObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
        //newBullet.transform.position = transform.position;
        newBullet.tag = "bullets";
        Debug.Log("new bullet created");

    }
	public void SetFireRate(float newRate) {
			this.fireRate = newRate;
				Debug.Log("new fire rate!");

	}
}