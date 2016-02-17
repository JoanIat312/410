using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour{
	
	int health = 15;
	float delay = 5f;
	public GameManager gameManager;
	int powerUpType;
    public BulletSpawn bulletSpawner;
    public float fireRate = .4f;


    void Awake(){
			
		}
		
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Player" && gameObject.name == "BigMac(Clone)"){
			gameManager.SendMessage("PlayerHealthPlus", health, SendMessageOptions.DontRequireReceiver);
            gameManager.SendMessage("ScoreTracker", 5, SendMessageOptions.DontRequireReceiver);
            //renderer.enabled = false;
            Destroy(gameObject);
		}
        if (col.gameObject.tag == "Player" && gameObject.name == "Gear(Clone)")
        {
            gameManager.SendMessage("PlayerShield", SendMessageOptions.DontRequireReceiver);
            gameManager.SendMessage("ScoreTracker", 5, SendMessageOptions.DontRequireReceiver);
            //renderer.enabled = false;
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Player" && gameObject.name == "machinegun(Clone)")
        {
            bulletSpawner.SendMessage("SetFireRate", fireRate, SendMessageOptions.DontRequireReceiver);
            gameManager.SendMessage("ScoreTracker", 5, SendMessageOptions.DontRequireReceiver);
            //renderer.enabled = false;
            Destroy(gameObject);
        }


    }
	
	
}
