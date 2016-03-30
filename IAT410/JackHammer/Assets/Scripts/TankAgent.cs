using UnityEngine;
using System.Collections;

public class TankAgent : MonoBehaviour {

	private NavMeshAgent agent;
	public GameManager gameManager;
	public GameObject bloodSpawner;
	private GameObject player;
	public float chaseSpeed = 2f;
	public State state;
	private bool alive;
	public float sightDist = 2;
	public GameObject bObject;
	public GameObject shotGunBulletObject;
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
		CHASE,
		ATTACK
	}

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player");
		agent.updatePosition = true;
		agent.updateRotation = false;
		alive = true;
		state = TankAgent.State.IDLE;
		StartCoroutine ("FSM");
		defaultStoppingDist = agent.stoppingDistance;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.stunEnemies == true) {
			agent.Stop ();

		} else {
			agent.Resume ();
		}
		dis = transform.position - player.transform.position;
		Debug.DrawRay (transform.position, -dis, Color.green);
		if (Physics.Raycast (transform.position, -dis, out hit, sightDist)) {
			if (hit.collider.gameObject.tag == "Player") {
				state = TankAgent.State.CHASE;
				if ((dis.z < firingRange && dis.z > -firingRange) && (dis.x < firingRange && dis.x > -firingRange)) {
					state = TankAgent.State.ATTACK;
				}
			}
		}
	}

	IEnumerator FSM ()
	{
		while (alive) {
			switch (state) {
			case State.CHASE:
				Chase ();
				break;
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
		agent.speed = 0;
	}

	void Chase ()
	{
		agent.speed = chaseSpeed;
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


	void Attack ()
	{
		agent.stoppingDistance = defaultStoppingDist; // reset the stopping distance

		agent.speed = chaseSpeed;
		agent.SetDestination (player.transform.position);
		if (Physics.Raycast (transform.position, -dis, out hit, sightDist)) {
			if (hit.collider.gameObject.tag == "wall") {
				state = TankAgent.State.CHASE;
			}
		}
		if (Time.time >= nextBulletSpawnTimestamp && GameManager.stunEnemies == false) {
			nextBulletSpawnTimestamp = Time.time + defaultFireRate;
			GameObject newBullet = Instantiate (bObject, sprite.transform.position, sprite.transform.rotation) as GameObject;
			for (int i = 0; i < 6; i++) {
				GameObject newShotBullet = Instantiate (shotGunBulletObject, sprite.transform.position, sprite.transform.rotation) as GameObject;
				newShotBullet.tag = "bullets";
				Debug.Log ("spwned");
			}
			AudioSource.PlayClipAtPoint (shot, transform.position);
			newBullet.tag = "bullets";

		}
	}

	void TakeDamage (int damage)
	{
		if (state == TankAgent.State.IDLE)
		{
			state = TankAgent.State.CHASE;
		}
		if (health - damage >= 0) {

			health -= damage;
			sprite.SendMessage ("TakeDamage", SendMessageOptions.DontRequireReceiver);
		} else {
			alive = false;
			bloodSpawner.SendMessage ("spawnDead", transform.position, SendMessageOptions.DontRequireReceiver);
			destory ();
			gameManager.SendMessage ("loadNextScene", SendMessageOptions.DontRequireReceiver);
		}
	}
		
	void destory ()
	{
		gameManager.SendMessage ("ScoreTracker", 150, SendMessageOptions.DontRequireReceiver);
		Destroy (sprite);
		Destroy (this.gameObject);

	}
}
