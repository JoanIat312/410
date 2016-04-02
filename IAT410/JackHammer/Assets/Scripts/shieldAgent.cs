using UnityEngine;
using System.Collections;

public class shieldAgent : MonoBehaviour {

	private NavMeshAgent agent;
	public GameObject sprite;
	public GameObject cap;
	public GameManager gameManager;
	public int defaultDamage = 30;
	GameObject[] objectsWithTag;
	GameObject closestObject = null;
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
			objectsWithTag = GameObject.FindGameObjectsWithTag ("EnemyAgent");
			closestObject = objectsWithTag [0];
			for (int i = 0; i < objectsWithTag.Length; i++) {
				//Debug.Log (objectsWithTag [i].name + ", " + Vector3.Distance (transform.position, objectsWithTag [i].transform.position));

				if (Vector3.Distance (transform.position, objectsWithTag [i].transform.position) <= Vector3.Distance (transform.position, closestObject.transform.position)) {
					closestObject = objectsWithTag [i];
				}
			}
			if ((Vector3.Distance (transform.position, closestObject.transform.position)) < 5f) {
				agent.SetDestination (new Vector3(closestObject.transform.position.x, 0.38f, closestObject.transform.position.z));
				//agent.stoppingDistance = 0;
			} else {
				agent.SetDestination (new Vector3(cap.transform.position.x,0.38f, cap.transform.position.z));
			}
		}
	}
		

	void OnTriggerEnter (Collider col)

	{
		if (gameObject.name != "shieldAgent") {
			if (col.gameObject.tag == "wall") {	
				Destroy(gameObject);
				Destroy (sprite);
			}
			if (col.gameObject.tag == "EnemyAgent") {	
				col.gameObject.SendMessage ("TakeDamage", defaultDamage, SendMessageOptions.DontRequireReceiver);
				Destroy(gameObject);
				Destroy (sprite);
			}
		}
	}
}
