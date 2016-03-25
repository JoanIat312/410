﻿using UnityEngine;
using System.Collections;

public class SubmarineAgent : MonoBehaviour {

	private NavMeshAgent agent;
	public GameManager gameManager;
	private GameObject player;
	public State state;
	private bool alive;
	public GameObject sprite;
	private Vector3 dis;
	private RaycastHit hit;
	public float firingRange = 2.5f;
	public float sightDist = 10;
	private float nextBulletSpawnTimestamp;
	public float defaultFireRate = .8f;
	public GameObject bObject;
	public AudioClip shot;

	public enum State
	{
		HIDE,
		POP,
		ATTACK
	}
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player");
		agent.updatePosition = true;
		agent.updateRotation = false;
		alive = true;
		state = SubmarineAgent.State.ATTACK;
		StartCoroutine ("FSM");
		defaultFireRate = 2f;
	}
	

	IEnumerator FSM ()
	{
		while (alive) {
			switch (state) {
			case State.HIDE:
				Hide ();
				break;
			case State.POP:
				Pop ();
				break;
			case State.ATTACK:
				Attack ();
				break;
			}

			yield return null;
		}
	}

	void Hide(){
		agent.speed = 0;
		sprite.SetActive (false);

	}

	void Pop(){
		sprite.SetActive (true);
	}

	void Attack(){
		if (Time.time >= nextBulletSpawnTimestamp && GameManager.stunEnemies == false) {
			nextBulletSpawnTimestamp = Time.time + defaultFireRate;
			GameObject newBullet = Instantiate (bObject, new Vector3(sprite.transform.position.x, 0.36f, sprite.transform.position.z), sprite.transform.rotation) as GameObject;
			AudioSource.PlayClipAtPoint (shot, transform.position);
			newBullet.tag = "bullets";
		}
	}

	void Update () {
		dis = transform.position - player.transform.position;
		Debug.DrawRay (transform.position, -dis, Color.green);
		if (Physics.Raycast (transform.position, -dis, out hit, sightDist)) {
			//Debug.Log (hit.collider.gameObject.tag);
			if (hit.collider.gameObject.tag == "Player") {
				//state = SubmarineAgent.State.POP;
				if ((dis.z < firingRange && dis.z > -firingRange) && (dis.x < firingRange && dis.x > -firingRange)) {
					state = SubmarineAgent.State.ATTACK;
				}
			} else {
				//state = SubmarineAgent.State.HIDE;
			}
		}
	}
}
