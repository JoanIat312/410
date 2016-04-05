using UnityEngine;
using System.Collections;

public class Trump : MonoBehaviour {
    public GameManager gameManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
     void TakeDamage (int damage)
     {
       gameManager.SendMessage ("loadNextScene", SendMessageOptions.DontRequireReceiver);
     }
}
