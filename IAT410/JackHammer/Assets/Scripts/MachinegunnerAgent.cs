﻿using UnityEngine;
using System.Collections;

public class MachinegunnerAgent : MonoBehaviour
{
//	private NavMeshAgent agent;
	public GameManager gameManager;
	private GameObject player;
	public GameObject bloodSpawner;
//	public float chaseSpeed = 2f;
	public State state;
	private bool alive;
	public float sightDist = 2;
	public GameObject bObject;
	public AudioClip shot;
	private Vector3 dis;
	public float defaultFireRate = .8f;
	private RaycastHit hit;
	private Vector3 shootingLocation;
	public GameObject sprite;
	private float nextBulletSpawnTimestamp;
	public float health;
	public float firingRange = 2.5f;
	private float defaultStoppingDist;

	public enum State
	{
		IDLE,
		ATTACK
	}
		
	// Use this for initialization
	void Start ()
	{
//		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player");
//		agent.updatePosition = true;
//		agent.updateRotation = false;
		alive = true;
        state = MachinegunnerAgent.State.IDLE;
		StartCoroutine ("FSM");
//		defaultStoppingDist = agent.stoppingDistance;
	}

	// Update is called once per frame
	void Update ()
	{
		if (GameManager.stunEnemies == true) {
//			agent.Stop ();
			sprite.SendMessage("Stunned", SendMessageOptions.DontRequireReceiver);

		} else {
//			agent.Resume ();
		}
		dis = transform.position - player.transform.position;
		Debug.DrawRay (transform.position, -dis, Color.green);
		if (Physics.Raycast (transform.position, -dis, out hit, sightDist)) {
			if (hit.collider.gameObject.tag == "Player") {
                //state = MachinegunnerAgent.State.CHASE;
				if ((dis.z < firingRange && dis.z > -firingRange) && (dis.x < firingRange && dis.x > -firingRange)) {
                 state = MachinegunnerAgent.State.ATTACK;
				}
			}
           else
           {
            state = MachinegunnerAgent.State.IDLE;
           }
		}
	}

	IEnumerator FSM ()
	{
		while (alive) {
			switch (state) {
			case State.IDLE:
				Idle ();
				break;
			case State.ATTACK:
				Attack ();
				break;
			}

			yield return null;
		}
	}

	void Idle ()
	{
//		agent.speed = 0;
	}


	void Attack ()
	{
//		agent.stoppingDistance = defaultStoppingDist; // reset the stopping distance
//
//		agent.speed = chaseSpeed;
//		agent.SetDestination (player.transform.position);
        
//        if (hit.collider != null)
//        {
        
//        }
		if (Time.time >= nextBulletSpawnTimestamp && GameManager.stunEnemies == false) {
			nextBulletSpawnTimestamp = Time.time + defaultFireRate;
			GameObject newBullet = Instantiate (bObject, sprite.transform.position, sprite.transform.rotation) as GameObject;
            // random flame sizes
//          newBullet.transform.localScale = new Vector3 (Random.Range (1, 5), Random.Range (1, 5), 1);
			AudioSource.PlayClipAtPoint (shot, transform.position);
			newBullet.tag = "bullets";
		}
	}

	void TakeDamage (float damage)
	{
        if (state == MachinegunnerAgent.State.IDLE)
        {
             state = MachinegunnerAgent.State.ATTACK;
        }
        if (health - damage >= 0) {
			health -= damage;
		    bloodSpawner.SendMessage ("spawn", transform.position, SendMessageOptions.DontRequireReceiver);
			
			sprite.SendMessage ("TakeDamage", SendMessageOptions.DontRequireReceiver);
		} else {
			alive = false;
			bloodSpawner.SendMessage ("spawnBigger", transform.position, SendMessageOptions.DontRequireReceiver);
			destroy ();
		}
	}

	void destroy ()
	{
        gameManager.SendMessage ("ScoreTracker", 30, SendMessageOptions.DontRequireReceiver);
        
		Destroy (sprite);
		Destroy (this.gameObject);
	}
}
