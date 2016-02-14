using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour{
	
	int health = 15;
	float delay = 5f;
	public GameManager gameManager;
	//public PowerUpSpawner s;
	//Vector3 startingPos;
	//float endPos;
	//public int unitsToMove = 5;
	//public int moveSpeed = 2 ;

	
	void Awake(){
				//transform.position = new Vector3(Random.Range(-8.5f, 7.5f), Random.Range(-6.5f,0.3f), 0);
				//endPos = startingPos - unitsToMove;
		}
		
		/*public IEnumerator Destory() {
			if (gameObject.tag == "clone") {
						yield return new WaitForSeconds (delay);
						Destroy (gameObject);
				}
			
		}*/
	
	/*void FixedUpdate(){
		if (rigidbody.position.y <= -20) {
			//Destroy (gameObject); 
			//renderer.enabled = false;
			rigidbody.position = s.transform.position;
			//renderer.enabled = true;
		}
	}*/
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Player"){
			//gameManager.SendMessage("PlayerHealthPlus", health, SendMessageOptions.DontRequireReceiver);
			//gameManager.movement.SendMessage("HealthIncreased", SendMessageOptions.DontRequireReceiver);
			//renderer.enabled = false;
			Destroy(gameObject);
		}

	}
	
	
}
