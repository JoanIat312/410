using UnityEngine;
using System.Collections;

public class shieldAgent : MonoBehaviour {

	private NavMeshAgent agent;
	public GameObject sprite;
	public GameObject cap;
	public GameManager gameManager;
	public int defaultDamage = 50;

	void Start () {
		agent = GetComponent < NavMeshAgent > ();
		agent.updateRotation = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		agent.stoppingDistance = 0;
		if (gameObject.name != "shieldA") {
			sprite.active = true;
			Vector3 closestEnemyPos = GetClosestEnemy ().transform.position;
			if ((Vector3.Distance (transform.position, closestEnemyPos)) < 10f) {
				agent.SetDestination (closestEnemyPos);
				agent.stoppingDistance = 0;
			} else {
				agent.SetDestination (cap.transform.position);
			}
		} else {
			agent.SetDestination (cap.transform.position);
			sprite.active = false;
		}
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

	void OnTriggerEnter (Collider col)

	{
		if (gameObject.name != "shieldA") {
			if (col.gameObject.tag == "wall") {	
				Destroy (gameObject, .4f);
			}
			if (col.gameObject.tag == "EnemyAgent") {	
				col.gameObject.SendMessage ("TakeDamage", defaultDamage, SendMessageOptions.DontRequireReceiver);
				Destroy (gameObject);
			}
		}
	}
}
