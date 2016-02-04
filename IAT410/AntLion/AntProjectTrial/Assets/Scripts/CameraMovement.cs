using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	Transform target;

	// Use this for initialization
	void Start () {

		target = GameObject.Find ("Ant_Player").transform;
	
	}
	
	// Update is called once per frame
	void Update () {

				transform.position = new Vector3 (target.position.x, target.position.y, -10);


	
	}


}
