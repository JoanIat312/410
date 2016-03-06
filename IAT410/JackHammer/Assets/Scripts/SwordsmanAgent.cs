using UnityEngine;
using System.Collections;

public class SwordsmanAgent : MonoBehaviour {
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 playerPos;
     public float zOffset;
     public float xOffset;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        playerPos = player.transform.position;

        agent.SetDestination(playerPos);
	}
}
