using UnityEngine;
using System.Collections;

public class shield : MonoBehaviour {

	public NavMeshAgent myAgent;
	public Transform target;
	public GameManager gameManager;
	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
