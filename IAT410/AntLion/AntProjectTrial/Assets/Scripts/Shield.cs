using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	float time = 1;
	//float delay = 5f;
	public GameManager gameManager;
	// Use this for initialization
	void Start () {
	
	}
	/*public IEnumerator Destory() {
		if (gameObject.tag == "clone") {
			yield return new WaitForSeconds (delay);
			Destroy (gameObject);
		}
		
	}*/
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Player"){
			gameManager.SendMessage("TimeIncrease", time, SendMessageOptions.DontRequireReceiver);
			//gameManager.movement.SendMessage("Healed", SendMessageOptions.DontRequireReceiver);
			
			Destroy (gameObject);
		}
		
	}
}
