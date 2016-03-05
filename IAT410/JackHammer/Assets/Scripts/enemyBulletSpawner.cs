using UnityEngine;
using System.Collections;

public class enemyBulletSpawner : MonoBehaviour {

	public GameObject bObject;
    public AudioClip shot;
    private float currentFireRate = .5f;
    private Vector3 targetPos;
    private Vector3 dis;
    public float defaultFireRate = .5f;


    private float nextBulletSpawnTimestamp;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Update()
    {
        targetPos = GameObject.Find("Player").transform.position;
        dis = transform.position - targetPos;
        dis.Normalize();
        if (dis.z < 0.5 && Time.time >= nextBulletSpawnTimestamp)
            {
                
                Spawn();
            }else{
              
            }
    }

    void Spawn()
    {
        nextBulletSpawnTimestamp = Time.time + currentFireRate;
        GameObject newBullet = Instantiate(bObject, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation) as GameObject;
        AudioSource.PlayClipAtPoint(shot, transform.position);
        newBullet.tag = "bullets";

    }

}