using UnityEngine;
using System.Collections;

public class CapAgent : MonoBehaviour {
	private NavMeshAgent agent;
	public GameObject sprite;
	private GameObject player;
	private Vector3 playerPos;
	public GameManager gameManager;
	public GameObject shieldAgent;
	private float spawnDelay = 15f;
	private float nextBulletSpawnTimestamp;
	// Use this for initialization
	void Start () {
		agent = GetComponent < NavMeshAgent > ();
		player = GameObject.Find ("Player");
		agent.updateRotation = false;
		//InvokeRepeating("spawn", spawnDelay, spawnDelay);
	}
	
	// Update is called once per frame
	void Update () {
		playerPos = player.transform.position;
		agent.SetDestination (playerPos);
		Vector3 closestEnemyPos = GetClosestEnemy ().transform.position;
         Debug.Log(Vector3.Distance (transform.position, closestEnemyPos));

		if ((Vector3.Distance (transform.position, closestEnemyPos)) < 5f) {
			if (Time.time >= nextBulletSpawnTimestamp) {
				spawn ();
			}
		}
	}

	void spawn(){
		//sprite.SendMessage ("shieldSpawn", SendMessageOptions.DontRequireReceiver);
		nextBulletSpawnTimestamp = Time.time + 1f;
		Vector3 objPos = new Vector3 (sprite.transform.position.x , 0.38f, sprite.transform.position.z);
		if (true) {
			GameObject newShield = Instantiate(shieldAgent, objPos, sprite.transform.rotation) as GameObject;
			newShield.tag = "clone";
		}

		//sprite.SendMessage ("shieldStop", SendMessageOptions.DontRequireReceiver);
	}

	GameObject GetClosestEnemy ()
	{
		// get array of all Enemy Agent objects    
		GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag ("EnemyAgent");
		GameObject closestObject = null;

		for (int i = 0; i < objectsWithTag.Length; i++) {
			if (closestObject == null) {
				closestObject = objectsWithTag [i];
				if (Vector3.Distance (transform.position, objectsWithTag [i].transform.position) <= Vector3.Distance (transform.position, closestObject.transform.position)) {
					closestObject = objectsWithTag [i];
				} 
			}
			//compares distances from player to each of the enemies

		}

		return closestObject;
	}
}
