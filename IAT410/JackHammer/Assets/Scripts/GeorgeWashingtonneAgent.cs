using UnityEngine;
using System.Collections;

public class GeorgeWashingtonneAgent: MonoBehaviour
{
	private NavMeshAgent agent;
	private GameObject player;
	private Vector3 playerPos;
	public GameManager gameManager;
	private bool rescued = false;
	// false is waiting to be found (does nothing), 1 is found (follows player and attacks enemies)
	private float distanceToTrigger = .9f;
	public float damagePerHit = 1f;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent < NavMeshAgent > ();
		player = GameObject.Find ("Player");
		agent.updateRotation = false;
//		agent.Stop ();
	}

	// Update is called once per frame
	void Update ()
	{

		playerPos = player.transform.position;
//		float distance = Vector3.Distance (playerPos, gameObject.transform.position);
////  Debug.Log("distance:"+distance);
//		if (rescued == false) {
//			if (distance < distanceToTrigger) {
//				rescued = true;
//				gameManager.SendMessage ("ScoreTracker", 100, SendMessageOptions.DontRequireReceiver);
//			}
//		} else {
//			agent.Resume ();
//			agent.SetDestination (playerPos);
//		}
		Vector3 closestEnemyPos = GetClosestEnemy ().transform.position;
		if ((Vector3.Distance (playerPos, closestEnemyPos)) < 4.5f) {
			agent.SetDestination (closestEnemyPos);
			agent.stoppingDistance = 0;
		} else {
			agent.SetDestination (playerPos);
			agent.stoppingDistance = 1;
		}
	}

	void OnCollisionStay (Collision collision)
	{
		if (collision.gameObject.tag == "EnemyAgent") {
			Debug.Log ("DAMAGED YOW!");

			collision.gameObject.SendMessage ("TakeDamage", damagePerHit, SendMessageOptions.DontRequireReceiver);
      
		}
	}

	void OnCollisionEnter (Collision col)
	{
		Debug.Log ("played");
		col.gameObject.SendMessage ("TakeDamage", damagePerHit, SendMessageOptions.DontRequireReceiver);
//      if (col.gameObject.tag == "EnemyAgent") {
//       Debug.Log("DAMAGED YOW!");
//
//       col.gameObject.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
//
//      }
	}

	GameObject GetClosestEnemy ()
	{
		// get array of all Enemy Agent objects    
		GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag ("EnemyAgent");
		GameObject closestObject = null;

		for (int i = 0; i < objectsWithTag.Length; i++) {
			if (closestObject == null) {
				closestObject = objectsWithTag [i];
				if (Vector3.Distance (playerPos, objectsWithTag [i].transform.position) <= Vector3.Distance (transform.position, closestObject.transform.position)) {
					closestObject = objectsWithTag [i];
				} 
			}
			//compares distances from player to each of the enemies

		}

		return closestObject;
	}
}