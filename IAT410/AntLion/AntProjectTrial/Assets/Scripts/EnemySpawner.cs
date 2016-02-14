using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
		private float minX;
		private float maxX;
		private float minY;
		private float maxY;
		private int spawnDelay;
		public GameObject Enemy;
	// Use this for initialization
	void Start () {
		minX = -16.5f;
		maxX = 12.5f;
		minY = -6.5f;
		maxY = 1.5f;
		spawnDelay = 2;

		InvokeRepeating ("SpawnEnemy", spawnDelay, spawnDelay);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void SpawnEnemy()
	{
		Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
		
		if (Physics.CheckSphere (position, .1f) == false)
		{ //You don't have something with a collider here
			GameObject newEnemy = Instantiate (Enemy, position, Quaternion.identity) as GameObject;
		}
	}



}
