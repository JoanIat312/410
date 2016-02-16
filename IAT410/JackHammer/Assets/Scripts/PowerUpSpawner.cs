using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {

	private Vector3 startPosition;

	public GameObject[] gameObjectSet;
	
	public float spawnDelay = 10f;

	public float spawn_position;

	// Use this for initialization
	void Start () {

        InvokeRepeating("spawnRandomObject", spawnDelay, spawnDelay);

    }

	void spawnRandomObject() 
	{
		int whichItem = Random.Range (0, 3);
		
		GameObject myObj = Instantiate (gameObjectSet[whichItem]) as GameObject;
		myObj.tag = "clone";
		
		myObj.transform.position = new Vector3(Random.Range(-11.7f, 12.74f), Random.Range(1.43f,-6.37f), 0);
		
	}

	
	// Update is called once per frame
	void Update () {

		//transform.position = new Vector3(Random.Range(-5, 7), Random.Range(-5,7), 0);
	
	}




}
