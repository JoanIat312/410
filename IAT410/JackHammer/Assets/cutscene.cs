using UnityEngine;
using System.Collections;

public class cutscene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine (LoadAfterAnim ());
	}

	IEnumerator LoadAfterAnim(){
		Debug.Log ("enter");
		yield return new WaitForSeconds (36);
		Application.LoadLevel (Application.loadedLevel+1);
	}
}
