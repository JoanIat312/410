using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour{
	
	int damage = 25;
	public GameManager gameManager;
	//public MultiSpawner s;
	
	Vector3 startingPos;
	float endPos;
	public int unitsToMove = 5;
	public int moveSpeed = 2 ;
	
	void Awake(){
		//startingPos = s.transform.position;
		//endPos = startingPos - unitsToMove;
		
	}
	void Update(){
		GetComponent<Rigidbody>().position += Vector3.down * moveSpeed * Time.deltaTime;
		
		/*if (rigidbody.position.y == startingPos.y) 
		{
			renderer.enabled = true;

		}

		if (rigidbody.position.y <= -20) {

			rigidbody.position = startingPos;

				}

		if (rigidbody.position.y <= endPos) {
			moveSpeed = 0;
		} */
	}
	
	/*void OnBecameInvisible(){

		Destroy (gameObject); 
		}
	*/
	
	//void FixedUpdate 
	
	
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			gameManager.SendMessage("PlayerDamage", damage, SendMessageOptions.DontRequireReceiver);
			gameManager.movement.SendMessage("Damaged", SendMessageOptions.DontRequireReceiver);
			
			Destroy (gameObject);
		}
		
		if (col.gameObject.tag == "Bottom") 
		{
			
			Destroy(gameObject);
			
			
		}
		
	}
	
	
}