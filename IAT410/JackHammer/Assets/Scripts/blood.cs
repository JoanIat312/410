using UnityEngine;
using System.Collections;

public class blood : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void play(){
		if (gameObject.name != "blood" || gameObject.name != "bloodSmall" || gameObject.name != "deadExpo"){
			Destroy (gameObject, .5f);
		}
	}
}
