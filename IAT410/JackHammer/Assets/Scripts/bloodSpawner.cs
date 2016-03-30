using UnityEngine;
using System.Collections;

public class bloodSpawner : MonoBehaviour {

	public GameObject[] bGameObjectSet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void spawn(Vector3 pos){
		GameObject newBlood = Instantiate (bGameObjectSet[0], new Vector3 (pos.x-.1f, pos.y+1f, pos.z), transform.rotation) as GameObject;
		newBlood.SendMessage ("play", SendMessageOptions.DontRequireReceiver);
	}

	void spawnBigger(Vector3 pos){
		GameObject newBlood = Instantiate (bGameObjectSet[1], new Vector3 (pos.x-.1f, pos.y+1f, pos.z), transform.rotation) as GameObject;
		newBlood.SendMessage ("play", SendMessageOptions.DontRequireReceiver);
	}

	void spawnDead(Vector3 pos){
		GameObject newBlood = Instantiate (bGameObjectSet[2], new Vector3 (pos.x-.1f, pos.y+1f, pos.z), transform.rotation) as GameObject;
		newBlood.SendMessage ("play", SendMessageOptions.DontRequireReceiver);
	}
}
