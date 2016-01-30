using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {

	private Vector3 startPosition;

	public GameObject[] gameObjectSet;

	public float timeLeftUnitlSpawn = 0f;
	public float startTime = 0f;
	
	public float secondsBetweenSpawn = 3f;

	public float spawn_position;

	// Use this for initialization
	void Start () {

		//startPosition = transform.position;
	
	}

	void spawnRandomObject() 
	{
		int whichItem = Random.Range (0, 2);
		
		GameObject myObj = Instantiate (gameObjectSet[whichItem]) as GameObject;
		myObj.tag = "clone";
		
		myObj.transform.position = new Vector3(Random.Range(-8.5f, 7.5f), Random.Range(-6.5f,0.3f), 0);
		
	}

	
	// Update is called once per frame
	void Update () {

		//transform.position = new Vector3(Random.Range(-5, 7), Random.Range(-5,7), 0);


		timeLeftUnitlSpawn = Time.time - startTime;
		
		if (timeLeftUnitlSpawn >= secondsBetweenSpawn) 
		{
			startTime = Time.time;
			timeLeftUnitlSpawn = 0;
			
			spawnRandomObject();
			
			
		}
	
	}




}
