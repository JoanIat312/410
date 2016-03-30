using UnityEngine;
using System.Collections;

public class shield : MonoBehaviour {

	public NavMeshAgent myAgent;
	public Transform target;
	public GameManager gameManager;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate() {
		transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
	}
}
