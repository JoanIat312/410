﻿using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

	public Transform target;
	public float zOffset;
	public float xOffset;
	//public int health;
	public GameManager gameManager;
	// need this but dont know why
	public NavMeshAgent myAgent;
	private Animator animator;
	void Start () {
		//health = 100;
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (myAgent.velocity.x == 0 && myAgent.velocity.z == 0) {
			animator.SetInteger ("Direction", 0);
		} else if (myAgent.velocity.z > .2f && myAgent.velocity.x > .2f) { // up and to the right
			animator.SetInteger ("Direction", 5);
		} else if (myAgent.velocity.z < -.2f && myAgent.velocity.x > .2f) { // down and to the right
			animator.SetInteger ("Direction", 6);
		} else if (myAgent.velocity.z < -.2f && myAgent.velocity.x < -.2f) { // down and to the left
			animator.SetInteger ("Direction", 7);
		} else if (myAgent.velocity.z > .2f && myAgent.velocity.x < -.2f) { // up and to the left
			animator.SetInteger ("Direction", 8);
		} else if (myAgent.velocity.z > .2f) { // up
			animator.SetInteger ("Direction", 1);
		} else if (myAgent.velocity.z < -.2f) { // down
			animator.SetInteger ("Direction", 3);
		} else if (myAgent.velocity.x > .2f) { // left
			animator.SetInteger ("Direction", 4);
		} else if (myAgent.velocity.x < -.2f) { //right
			animator.SetInteger ("Direction", 2);
		}
	}
	void LateUpdate ()
	{
		transform.position = new Vector3 (target.position.x, transform.position.y, target.position.z);
	}

	IEnumerator TakeDamage ()
	{
		// flash enemy when hit
		GetComponent<SpriteRenderer> ().color = new Color (255f, 0f, 0f);
		yield return new WaitForSeconds (0.1f); 
		GetComponent<SpriteRenderer> ().color = new Color (255f, 255f, 255f);
	}
}
