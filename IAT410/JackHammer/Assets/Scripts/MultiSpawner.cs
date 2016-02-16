using UnityEngine;
using System.Collections;

public class MultiSpawner: MonoBehaviour {
	//Rock rocks;
	private Vector3 startPosition;

	private Vector3 newXPos;

	public float moveSpeed = 1f;

	public float moveDistance = 4f;
	public GameObject[] gameObjectSet;

	public float timeLeftUnitlSpawn = 0f;
	public float startTime = 0f;

	public float secondsBetweenSpawn = 3f;

	int damage = 25;

	// Use this for initialization
	void Start () {

		//InvokeRepeating("spawnRandomObject", Random.Range (1,5), Random.Range (1,5));

		startPosition = transform.position;
	
	}

	void spawnRandomObject() 
	{
		int whichItem = Random.Range (0,gameObjectSet.Length);
		
		GameObject myObj = Instantiate (gameObjectSet[whichItem]) as GameObject;
		//myObj.tag = "clone";
		
		myObj.transform.position = transform.position;


	}
	void FixedUpdate(){

		newXPos = new Vector3(Random.Range(-8.5f, 7.5f), 0.3f, 0f);
	}
	
	// Update is called once per frame
	void Update () {

		//newXPos++;
		//secondsBetweenSpawn -= Random.Range (0.001f, 0.08f);
		//newXPos = Mathf.PingPong (Time.time * moveSpeed, moveDistance) - (moveDistance/2f);

		transform.position = newXPos;

		timeLeftUnitlSpawn = Time.time - startTime;

		if (timeLeftUnitlSpawn >= secondsBetweenSpawn) 
		{
			startTime = Time.time;
			//secondsBetweenSpawn = 5;
			timeLeftUnitlSpawn = 0;

			spawnRandomObject();
				
		
		}

	
	}
}
