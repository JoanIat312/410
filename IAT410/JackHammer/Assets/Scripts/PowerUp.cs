using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
	
	int health = 15;
	float delay = 5f;
	public GameManager gameManager;
	int powerUpType;
	public AudioClip pickup;
	public playerBulletSpawner bulletSpawner;
	//public float fireRate = .4f;


	void Awake ()
	{
			
	}

	
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Player" && gameObject.name == "BigMac(Clone)") {
			AudioSource.PlayClipAtPoint (pickup, transform.position);
			gameManager.SendMessage ("PlayerHealthPlus", health, SendMessageOptions.DontRequireReceiver);
			gameManager.SendMessage ("ScoreTracker", 5, SendMessageOptions.DontRequireReceiver);
			//renderer.enabled = false;
			Destroy (gameObject);
		}
		if (col.gameObject.tag == "Player" && gameObject.name == "Gear(Clone)") {
			AudioSource.PlayClipAtPoint (pickup, transform.position);
			gameManager.SendMessage ("PlayerShield", SendMessageOptions.DontRequireReceiver);
			gameManager.SendMessage ("ScoreTracker", 5, SendMessageOptions.DontRequireReceiver);
			//renderer.enabled = false;
			Destroy (gameObject);
		}

		if (col.gameObject.tag == "Player" && gameObject.name == "machinegun(Clone)") {
			AudioSource.PlayClipAtPoint (pickup, transform.position);
			bulletSpawner.SendMessage ("SetWeapon", 1, SendMessageOptions.DontRequireReceiver);
			gameManager.SendMessage ("ScoreTracker", 5, SendMessageOptions.DontRequireReceiver);
			//renderer.enabled = false;
			Destroy (gameObject);
		}
        
		if (col.gameObject.tag == "Player" && gameObject.name == "shotgun-placeholder(Clone)") {
			AudioSource.PlayClipAtPoint (pickup, transform.position);
			bulletSpawner.SendMessage ("SetWeapon", 2, SendMessageOptions.DontRequireReceiver);
			gameManager.SendMessage ("ScoreTracker", 5, SendMessageOptions.DontRequireReceiver);
			//renderer.enabled = false;
			Destroy (gameObject);
		}
	}
	
	
}
