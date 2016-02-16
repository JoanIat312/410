using UnityEngine;
using System.Collections;

public class Bottom : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		
		if (collision.gameObject.tag == "Bottom") 
		{

			Destroy(gameObject);
			
			
		}
	}
}
