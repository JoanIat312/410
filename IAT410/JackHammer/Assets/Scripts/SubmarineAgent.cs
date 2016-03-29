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
	public float health;
	private float defaultStoppingDist;
	public enum State
	{
		HIDE,
		POP,
		ATTACK,
		CHASE
	}
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player");
		agent.updatePosition = true;
		agent.updateRotation = false;
		alive = true;
		state = SubmarineAgent.State.HIDE;
		StartCoroutine ("FSM");
		defaultFireRate = 2f;
		defaultStoppingDist = agent.stoppingDistance;
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
			case State.CHASE:
				Chase ();
				break;
			}

			yield return null;
		}
	}

	void Hide(){
		Debug.Log ("hide");
		agent.speed = 0;
		//sprite.SendMessage ("setHide", SendMessageOptions.DontRequireReceiver);
		sprite.SetActive (false);
		//a.SetInteger ("Direction", -2);


	}

	void Chase(){
		sprite.SetActive (true);
		agent.speed = 10;
		//Debug.Log("startChase");
		agent.SetDestination (player.transform.position);
		if (Physics.Raycast (transform.position, -dis, out hit, sightDist)) {
			if (hit.collider.gameObject.tag != "Player") { // if the enemy still cant see the player
				//go right up to him
				agent.stoppingDistance = .7f;
			} else {
				agent.stoppingDistance = defaultStoppingDist; // reset the stopping distance
			}
		}

	}
	void Pop(){
		Debug.Log ("pop");
		agent.speed = 0;
		sprite.SetActive (true);
		//sprite.SendMessage ("setPop", SendMessageOptions.DontRequireReceiver);
	}

	void Attack(){
		sprite.SetActive (true);
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
			if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.name == "bullets(Clone)") {
				state = SubmarineAgent.State.POP;
				if ((dis.z < firingRange && dis.z > -firingRange) && (dis.x < firingRange && dis.x > -firingRange)) {
					state = SubmarineAgent.State.ATTACK;
				} else {
					state = SubmarineAgent.State.CHASE;
				}
			} else {
				state = SubmarineAgent.State.HIDE;
			}
		} else {
			//state = SubmarineAgent.State.HIDE;
		}
	}

	void TakeDamage (int damage)
	{
		if (health - damage >= 0) {

			health -= damage;

			sprite.SendMessage ("TakeDamage", SendMessageOptions.DontRequireReceiver);
			//state = SubmarineAgent.State.HIDE;
		} else {
			alive = false;

			destory ();
			//gameManager.SendMessage ("loadNextScene", SendMessageOptions.DontRequireReceiver);
		}
	}

	void destory ()
	{
		gameManager.SendMessage ("ScoreTracker", 150, SendMessageOptions.DontRequireReceiver);
		Destroy (sprite);
		Destroy (this.gameObject);

	}
}