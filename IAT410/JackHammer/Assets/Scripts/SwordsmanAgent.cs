using UnityEngine;
using System.Collections;

public class SwordsmanAgent : MonoBehaviour {
    private NavMeshAgent agent;
	public GameManager gameManager;
    private GameObject player;
	public GameObject bloodSpawner;
	public float chaseSpeed = 1f;
	public State state;
	private bool alive;
	public float sightDist = 2;
	public GameObject bObject;
	public AudioClip shot;
	private Vector3 dis;
//	public float defaultFireRate = .8f;
	private RaycastHit hit;
	private Vector3 shootingLocation;
	public GameObject sprite;
	private float nextBulletSpawnTimestamp;
	public float health;
	public enum State
	{
		IDLE,
		CHASE
	}


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
		player = GameObject.Find ("Player");
		agent.updatePosition = true;
		agent.updateRotation = false;
		alive = true;
		state = SwordsmanAgent.State.IDLE;
		StartCoroutine ("FSM");
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.stunEnemies == true) {
			agent.Stop ();
			sprite.SendMessage("Stunned", SendMessageOptions.DontRequireReceiver);
		}
		else {
			agent.Resume ();
		}
		dis = transform.position - player.transform.position;
		//Debug.DrawRay (transform.position, -dis, Color.green);
		if (Physics.Raycast (transform.position, -dis, out hit, sightDist)) {
			if (hit.collider.gameObject.tag == "Player") {
				state = SwordsmanAgent.State.CHASE;
				if ((dis.z < 1.5 && dis.z > -1.5) && (dis.x < 1.5 && dis.x > -1.5)) {
					//state = SwordsmanAgent.State.ATTACK;
				}
			}
		}
	}

	IEnumerator FSM(){
		while (alive) {
			switch (state) {
				case State.CHASE:
					Chase ();
					break;
				case State.IDLE:
					Idle ();
					break;
			}

			yield return null;
		}
	}

	void Idle() {
		agent.speed = 0;
	}

	void Chase() {
		agent.speed = chaseSpeed;
		agent.SetDestination (player.transform.position);
	}


	void TakeDamage(float damage){
//        Debug.Log(health);
        if (state == SwordsmanAgent.State.IDLE)
        {
         state = SwordsmanAgent.State.CHASE;
        }
		if (health - damage >= 0) {
			health -= damage;
			bloodSpawner.SendMessage("spawn", transform.position, SendMessageOptions.DontRequireReceiver);
			sprite.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
		} else {
			alive = false;
			bloodSpawner.SendMessage("spawnBigger", transform.position, SendMessageOptions.DontRequireReceiver);
			destroy ();
		}
	}

 void destroy()
	{
		gameManager.SendMessage("ScoreTracker", 10, SendMessageOptions.DontRequireReceiver);
		Destroy (sprite);
		Destroy(this.gameObject);

	}
}
