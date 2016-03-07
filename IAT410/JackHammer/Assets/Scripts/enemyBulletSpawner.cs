﻿using UnityEngine;
using System.Collections;

public class enemyBulletSpawner : MonoBehaviour {

	public GameObject bObject;
    public AudioClip shot;
    private float currentFireRate = .8f;
    private Vector3 targetPos;
    private Vector3 dis;
    public float defaultFireRate = .8f;
	private bool blocked;


    private float nextBulletSpawnTimestamp;
    // Use this for initialization
    void Start()
    {
		transform.position = GameObject.Find ("Swordsman").transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Update()
    {
        targetPos = GameObject.Find("Player").transform.position;
        dis = transform.position - targetPos;
        //dis.Normalize();
        //Debug.Log("player dis" + dis);
		if (Time.time >= nextBulletSpawnTimestamp && blocked == false)
            {
                if((dis.z < 2.4 && dis. z > -2.4) && (dis.x < 2.4 && dis.x > -2.4 )){
                    //Debug.Log("player in sight, start firing");
                    //Spawn();
                }else{
                    //Debug.Log("player not in sight, stop firing");
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

	void blockDetection(bool b){
		blocked = b;
		Debug.Log ("Received: " + b);
	}

}