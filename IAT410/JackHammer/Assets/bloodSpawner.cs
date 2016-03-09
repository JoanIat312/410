using UnityEngine;
using System.Collections;

public class bloodSpawner : MonoBehaviour {

	public GameObject bloodObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void spawn(Vector3 pos){

		GameObject newBlood = Instantiate (bloodObject, new Vector3 (pos.x-.1f, 1f, pos.z), transform.rotation) as GameObject;
		newBlood.SendMessage ("play", SendMessageOptions.DontRequireReceiver);
	}
}
