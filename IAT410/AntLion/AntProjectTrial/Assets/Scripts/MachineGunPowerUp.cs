using UnityEngine;
using System.Collections;

public class MachineGunPowerUp : MonoBehaviour {

	public BulletSpawn bulletSpawner;
	public float fireRate = .4f;


	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Player"){
				bulletSpawner.SendMessage("SetFireRate", fireRate, SendMessageOptions.DontRequireReceiver);
				//gameManager.movement.SendMessage("HealthIncreased", SendMessageOptions.DontRequireReceiver);
				//renderer.enabled = false;
				Destroy(gameObject);
		}
	}
	
}

