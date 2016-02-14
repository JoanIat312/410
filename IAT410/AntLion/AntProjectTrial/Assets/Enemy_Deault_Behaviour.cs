using UnityEngine;
using System.Collections;

public class Enemy_Deault_Behaviour : MonoBehaviour {
	private Vector3 Player;
	private Vector2 Playerdirection;
	private float xDif;
	private float yDif;
	public float speed;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		speed = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Player = GameObject.Find ("Ant_Player").transform.position;
		xDif = Player.x - transform.position.x;
		yDif = Player.y - transform.position.y;

		Playerdirection = new Vector3 (xDif, yDif, 1);
		rb.AddForce (Playerdirection.normalized * speed);
	}
}
