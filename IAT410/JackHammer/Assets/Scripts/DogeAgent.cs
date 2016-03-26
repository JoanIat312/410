using UnityEngine;
using System.Collections;

public class DogeAgent : MonoBehaviour {

	private NavMeshAgent agent;
	public GameObject sprite;
	private GameObject player;
	private Vector3 playerPos;
	public GameManager gameManager;
	public GameObject burger;
	private float spawnDelay = 15f;
	void Start () {
		agent = GetComponent < NavMeshAgent > ();
		player = GameObject.Find ("Player");
		agent.updateRotation = false;
		InvokeRepeating("spawn", spawnDelay, spawnDelay);
	}
	
	// Update is called once per frame
	void Update () {
		playerPos = player.transform.position;
		agent.SetDestination (playerPos);
	

	}

	void OnCollisionEnter (Collision col){
		

	}
	void spawn(){

		Vector3 objPos = new Vector3 (transform.position.x , 0.39f, sprite.transform.position.z);
		if (true) {
			GameObject newBurger = Instantiate(burger, objPos, sprite.transform.rotation) as GameObject;
			newBurger.tag = "clone";
			Debug.Log ("spawn enter");
		}
	}
}
