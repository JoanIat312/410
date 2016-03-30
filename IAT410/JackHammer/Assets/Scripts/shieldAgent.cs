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
		transform.position = new Vector3 (transform.position.x, 0.38f, transform.position.z);
//		Debug.Log (transform.position.y);
		if (gameObject.name != "shieldAgent") {
			Vector3 closestEnemyPos = GetClosestEnemy ().transform.position;
			if ((Vector3.Distance (transform.position, closestEnemyPos)) < 17f) {
				agent.SetDestination (new Vector3(closestEnemyPos.x, 0.38f, closestEnemyPos.z));
				//agent.stoppingDistance = 0;
			} else {
				agent.SetDestination (new Vector3(cap.transform.position.x,0.38f, cap.transform.position.z));
			}
		} else {
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
		if (gameObject.name != "shieldAgent") {
			if (col.gameObject.tag == "wall") {	
				Destroy (gameObject, .4f);
				Destroy (sprite);
			}
			if (col.gameObject.tag == "EnemyAgent") {	
				col.gameObject.SendMessage ("TakeDamage", defaultDamage, SendMessageOptions.DontRequireReceiver);
				Debug.Log ("entered");
				Destroy (gameObject);
				Destroy (sprite);
			}
		}
	}
}
