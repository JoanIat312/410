using UnityEngine;
using System.Collections;

public class SubmarineAgent : MonoBehaviour {

	private NavMeshAgent agent;
	public GameManager gameManager;
	private GameObject player;
	public GameObject bloodSpawner;
	public State state;
	private bool alive;
	public GameObject sprite;
	private Vector3 dis;
	private RaycastHit hit;
	public float firingRange = 2.5f;
	public float sightDist = 10;
	private float nextBulletSpawnTimestamp;
	private float nextRocketSpawnTimestamp;
	public float defaultFireRate = .8f;
	public GameObject bObject;
	public GameObject shotGunBulletObject;
	public AudioClip shot;
	public float health;
	private float defaultStoppingDist;
    private float timeForNextAttack = 0;
    private float timeForHide = 0;
 private float timeToAttack = 5f;
 private float timeToHide = 3f;
 private bool popAnimationPlayed = false;
 private bool hideAnimationPlayed = false;

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
		agent.speed = 0;
		//sprite.SendMessage ("setHide", SendMessageOptions.DontRequireReceiver);
		sprite.SetActive (false);
		//a.SetInteger ("Direction", -2);


	}

	void Chase(){
		sprite.SetActive (true);
		agent.speed = 1;
		//Debug.Log("startChase");
		agent.SetDestination (player.transform.position);
		//if (Physics.Raycast (transform.position, -dis, out hit, sightDist)) {
			//if (hit.collider.gameObject.tag != "Player") { // if the enemy still cant see the player
				//go right up to him
				agent.stoppingDistance = 2f;
//			} else {
//				agent.stoppingDistance = defaultStoppingDist; // reset the stopping distance
//			}
//		}

	}
	void Pop(){
		Debug.Log ("pop");
		agent.speed = 0;
		sprite.SetActive (true);
		//sprite.SendMessage ("setPop", SendMessageOptions.DontRequireReceiver);
	}

	void Attack(){
        agent.speed = 1;
        agent.SetDestination (player.transform.position);
        agent.stoppingDistance = 2f;
		sprite.SetActive (true);
		if (Time.time >= nextRocketSpawnTimestamp && GameManager.stunEnemies == false) {
			nextRocketSpawnTimestamp = Time.time + defaultFireRate*3;
			GameObject newBullet = Instantiate (bObject, new Vector3(sprite.transform.position.x, 0.36f, sprite.transform.position.z), sprite.transform.rotation) as GameObject;
			AudioSource.PlayClipAtPoint (shot, transform.position);
			newBullet.tag = "bullets";
		}
		if (Time.time >= nextBulletSpawnTimestamp && GameManager.stunEnemies == false) {
			nextBulletSpawnTimestamp = Time.time + defaultFireRate;
			for (int i = 0; i < 8; i++) {
				GameObject newShotBullet = Instantiate (shotGunBulletObject, sprite.transform.position, sprite.transform.rotation) as GameObject;
				newShotBullet.tag = "bullets";
			}
		}
	}

	void Update () {
		dis = transform.position - player.transform.position;
		Debug.DrawRay (transform.position, -dis, Color.green);

		//if (Physics.Raycast (transform.position, -dis, out hit, sightDist)) {
			//Debug.Log (hit.collider.gameObject.tag);
			//if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.name == "bullets(Clone)") {
              if (Time.time >= timeForNextAttack && Time.time < timeForNextAttack + timeToAttack) {
                timeForHide = Time.time + timeToHide;
                if (!popAnimationPlayed)
                {
                     sprite.SendMessage("hideAnimation", SendMessageOptions.DontRequireReceiver);
                     popAnimationPlayed = true;
                     hideAnimationPlayed = false;
                }
				//state = SubmarineAgent.State.POP;
				if ((dis.z < firingRange && dis.z > -firingRange) && (dis.x < firingRange && dis.x > -firingRange)) {
					state = SubmarineAgent.State.ATTACK;
				} else {
					state = SubmarineAgent.State.CHASE;
				}
              } else if (Time.time >= timeForHide && Time.time < timeForHide + timeToHide){
                    if (!hideAnimationPlayed)
                    {
                         sprite.SendMessage("hideAnimation", SendMessageOptions.DontRequireReceiver);
                         hideAnimationPlayed = true;
                         popAnimationPlayed = false;
                    }
                   // sprite.SendMessage ("hideAnimation", SendMessageOptions.DontRequireReceiver);
            	    state = SubmarineAgent.State.HIDE;
                    timeForNextAttack = Time.time + timeToAttack;
			    }   
//		} else {
			//state = SubmarineAgent.State.HIDE;
//		}
	}
    

	void TakeDamage (int damage)
	{
		if (health - damage >= 0) {

			health -= damage;

			sprite.SendMessage ("TakeDamage", SendMessageOptions.DontRequireReceiver);
			//state = SubmarineAgent.State.HIDE;
		} else {
			alive = false;
			bloodSpawner.SendMessage ("spawnDead", transform.position, SendMessageOptions.DontRequireReceiver);
			destroy ();
		}
	}
	void destroy ()
	{
		gameManager.SendMessage ("loadWin", SendMessageOptions.DontRequireReceiver);
		gameManager.SendMessage ("ScoreTracker", 150, SendMessageOptions.DontRequireReceiver);
		Destroy (sprite);
		Destroy (this.gameObject);

	}


}
