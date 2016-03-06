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
       transform.position = GameObject.Find("Swordsman").transform.position;
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
        Debug.Log("player dis" + dis);
        if (Time.time >= nextBulletSpawnTimestamp)
            {
                if((dis.z < 0.4 && dis. z > -0.4) || (dis.x < 0.5 && dis.x > -0.5 )){
                    Spawn();
                }
            }
    }

    void Spawn()
    {
        nextBulletSpawnTimestamp = Time.time + currentFireRate;
        GameObject newBullet = Instantiate(bObject, new Vector3(transform.position.x, 0.38f, transform.position.z), transform.rotation) as GameObject;
        AudioSource.PlayClipAtPoint(shot, transform.position);
        newBullet.tag = "bullets";

    }

}