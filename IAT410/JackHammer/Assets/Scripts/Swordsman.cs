using UnityEngine;
using System.Collections;

public class Swordsman : MonoBehaviour {
    
     public Transform target;
     public float zOffset;
     public float xOffset; // need this but dont know why
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate() {
        transform.localPosition = new Vector3(target.localPosition.x + xOffset, transform.localPosition.y, target.localPosition.z + zOffset);
    }
}
