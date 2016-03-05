using UnityEngine;
using System.Collections;

public class SwordsmanAgent : MonoBehaviour {
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 playerPos;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        playerPos = player.transform.position;

          agent.SetDestination(playerPos);
//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;
//                        
//            if (Physics.Raycast(ray, out hit))
//            {
//                if (hit.collider.tag == "Ground")
//                {
//                 agent.SetDestination(hit.point);
//                }
//            }
//        }
	}
}
