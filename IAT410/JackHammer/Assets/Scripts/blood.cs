using UnityEngine;
using System.Collections;

public class blood : MonoBehaviour {

	Animator anim;
	public AudioClip hurt;
	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void play(){
		Debug.Log (transform.position + " played"); 
		if (gameObject.name != "blood"){
			Destroy (gameObject, .5f);
		}
	}
}
