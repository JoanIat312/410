using UnityEngine;
using System.Collections;

public class SwordsmanAgent : MonoBehaviour {
    private NavMeshAgent agent;
    private GameObject player;
	public float chaseSpeed = 1f;
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
	public enum State
	{
		IDLE,
		CHASE,
		ATTACK
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
	}
	
	// Update is called once per frame
	void Update () {
        //playerPos = player.transform.position;
        /*agent.SetDestination(playerPos);
		if (Physics.Raycast (transform.position, playerPos - transform.position, out hit, col.radius)) {
			blocked = true;
			Debug.Log ("true");
		} else {
			blocked = false;
		}
		s.SendMessage("blockDetection", blocked, SendMessageOptions.DontRequireReceiver);*/
		dis = transform.position - player.transform.position;
		Debug.DrawRay (transform.position, -dis, Color.green);
		//Debug.DrawRay (transform.position, (transform.forward + transform.right).normalized * sightDist, Color.green);
		//Debug.DrawRay (transform.position, (transform.forward - transform.right).normalized * sightDist, Color.green);
		if (Physics.Raycast (transform.position, -dis, out hit, sightDist)) {
			if (hit.collider.gameObject.tag == "Player") {
				state = SwordsmanAgent.State.CHASE;
				if ((dis.z < 1.5 && dis.z > -1.5) && (dis.x < 1.5 && dis.x > -1.5)) {
					state = SwordsmanAgent.State.ATTACK;

				}
			}
		} else {
			state = SwordsmanAgent.State.IDLE;
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
				case State.ATTACK:
					Attack ();
					break;
			}

			yield return null;
		}
	}

	void Idle() {
		agent.speed = 0;
		/*agent.speed = patrolSpeed;
		if (Vector3.Distance (transform.position, waypoints [wayPointInd].transform.position) >= 2) {
			agent.SetDestination (waypoints [wayPointInd].transform.position);
		} else if (Vector3.Distance (transform.position, waypoints [wayPointInd].transform.position) <= 2) {
			wayPointInd += 1;
			if (wayPointInd > waypoints.Length) {
				wayPointInd = 0;
			}
		} else {
			//agent.speed = 0;
		}*/
	}

	void Chase() {
		agent.speed = chaseSpeed;
		agent.SetDestination (player.transform.position);
	}
	void Attack(){
		if (hit.collider.gameObject.tag == "wall") {
			state = SwordsmanAgent.State.CHASE;
		}
		if (Time.time >= nextBulletSpawnTimestamp) {
			nextBulletSpawnTimestamp = Time.time + defaultFireRate;
			GameObject newBullet = Instantiate (bObject, sprite.transform.position, sprite.transform.rotation) as GameObject;
			AudioSource.PlayClipAtPoint (shot, transform.position);
			newBullet.tag = "bullets";
		}
	}
	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			state = SwordsmanAgent.State.CHASE;
			player = col.gameObject;
		}
	}
}
