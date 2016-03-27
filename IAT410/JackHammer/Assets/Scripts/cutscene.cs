using UnityEngine;
using System.Collections;

public class cutscene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//StartCoroutine (LoadAfterAnim ());
		if(Input.GetMouseButton (0)){
			Application.LoadLevel (Application.loadedLevel+1);
		}
	}

	IEnumerator LoadAfterAnim(){
		yield return new WaitForSeconds (45);
		Application.LoadLevel (Application.loadedLevel+1);
	}
		
}
