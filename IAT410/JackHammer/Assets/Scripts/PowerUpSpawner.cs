using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {

	private Vector3 startPosition;

	public GameObject[] gameObjectSet;
	
	public float spawnDelay = 1f;

	public float spawn_position;

    public float xStart = -20.25f;
    public float xEnd = 1.58f;
    public float zStart = -6.11f;
    public float zEnd = 5.87f;

	// Use this for initialization
	void Start () {

        InvokeRepeating("spawnRandomObject", spawnDelay, spawnDelay);

    }

    void spawnRandomObject()
    {
        int whichItem = Random.Range(0, 4);

        Vector3 objPos = new Vector3(Random.Range(xStart, xEnd), .38f, Random.Range(zStart, zEnd));
        if (Physics.CheckSphere(objPos, .3f) == false) {
            GameObject myObj = Instantiate(gameObjectSet[whichItem], objPos, transform.rotation) as GameObject;
            myObj.tag = "clone";
        }
	}

	
	// Update is called once per frame
	void Update () {

		//transform.position = new Vector3(Random.Range(-5, 7), Random.Range(-5,7), 0);
	
	}




}
