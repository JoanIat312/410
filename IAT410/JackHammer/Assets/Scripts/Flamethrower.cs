﻿using UnityEngine;
using System.Collections;

public class Flamethrower : MonoBehaviour {
	public Transform target;
	public float zOffset;
	public float xOffset; 
	//public int health;
	//public GameManager gameManager;// need this but dont know why
	public NavMeshAgent myAgent;
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (myAgent.velocity.x == 0 && myAgent.velocity.z == 0)
        {
            animator.SetInteger("Direction", 0);
        }
        else if (myAgent.velocity.x > .2f) { // left
            animator.SetInteger ("Direction", 2);
        } else if (myAgent.velocity.x < -.2f) { //right
            animator.SetInteger ("Direction", 4);
        }
		else if (myAgent.velocity.z > .2f) { // up
			animator.SetInteger ("Direction", 1);
		} else if (myAgent.velocity.z < -.2f) { // down
			animator.SetInteger ("Direction", 3);
		}
	}

	void LateUpdate() {
		transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
	}

	IEnumerator TakeDamage() {
		// flash enemy when hit
		GetComponent<SpriteRenderer> ().color = new Color (255f, 0f, 0f);
		yield return new WaitForSeconds(0.1f); 
		GetComponent<SpriteRenderer> ().color = new Color (255f, 255f, 255f);
	}

	IEnumerator Stunned() {
		// flash enemy when hit
		GetComponent<SpriteRenderer> ().color = new Color (0f, 213f, 244f);
		yield return new WaitForSeconds(0.8f); 
		GetComponent<SpriteRenderer> ().color = new Color (255f, 255f, 255f);
	}
}
