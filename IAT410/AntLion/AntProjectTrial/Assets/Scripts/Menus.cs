using UnityEngine;
using System.Collections;

public class Menus : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// function to load levels when buttons pressed
	public void LoadLevel(string level) {
		Application.LoadLevel (level);
	}   
	// quit game
	public void QuitApp() {
		Application.Quit ();
	}
}
