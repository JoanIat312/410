using UnityEngine;
using System.Collections;

public class playerBulletSpawner : MonoBehaviour {

		public GameObject bObject;
		public GameObject shotGunBulletObject;

		public AudioClip shot;
		public int equippedGun = 2;
		// active weapon status - 0 is default, 1 is fast shootng machinegun, 2 is shotgun

		public float defaultFireRate = .5f;
		public float currentFireRate = .5f;
		public float machineGunFireRate = .2f;
		public float shotGunFireRate = .6f;
		public int machineGunBullets = 0;
		public int shotGunBullets = 0;

		private float nextBulletSpawnTimestamp;
		// Use this for initialization
		void Start ()
		{
			transform.position = GameObject.Find ("Player").transform.position;
			//fireRate = .5f;
		}

		// Update is called once per frame
		void FixedUpdate ()
		{
		}

		void Update ()
		{


			if ((Input.GetMouseButton (0)) && (Time.time >= nextBulletSpawnTimestamp)) {
//				Debug.Log ("left pressed");
				Spawn ();
			}
		}

		void Spawn ()
		{
			nextBulletSpawnTimestamp = Time.time + currentFireRate;

			if ((equippedGun == 1) && (machineGunBullets > 0)) {
				machineGunBullets -= 1;
				//Debug.Log (machineGunBullets);
				if (machineGunBullets <= 0) {
					// go back to default gun if not shotgun bullets either
                    if (shotGunBullets > 0)
                    {
                        SetWeapon(2);
                    }
                    else
                    {
                        SetWeapon(0);
                    }
				}
				GameObject newBullet = Instantiate (bObject, new Vector3 (transform.position.x, 0.38f, transform.position.z), transform.rotation) as GameObject;
				newBullet.tag = "bullets";      
			}

			if ((equippedGun == 2) && (shotGunBullets > 0)) {
				shotGunBullets -= 1;
				//Debug.Log (shotGunBullets);
				if (shotGunBullets <= 0) {
                    // go back to default gun if not machinegun bullets either
                    if (machineGunBullets > 0)
                    {
                     SetWeapon(1);
                    }
                    else
                    {
                     SetWeapon(0);
                    }
				}
				// spawn 6 bullets
				for (int i = 0; i < 6; i++) {
					GameObject newBullet = Instantiate (shotGunBulletObject, new Vector3 (transform.position.x, 0.38f, transform.position.z), transform.rotation) as GameObject;
					newBullet.tag = "bullets";
				}
			} else {
				GameObject newBullet = Instantiate (bObject, new Vector3 (transform.position.x, 0.38f, transform.position.z), transform.rotation) as GameObject;
				newBullet.tag = "bullets";    
			}

			AudioSource.PlayClipAtPoint (shot, transform.position);
			//Debug.Log("new bullet created");

		}

		public void SetWeapon (int newWeapon)
		{
			this.equippedGun = newWeapon;

			if (equippedGun == 0) {
				currentFireRate = defaultFireRate;
			} else if (equippedGun == 1) {
				machineGunBullets += 30;
				currentFireRate = machineGunFireRate;
			} else if (equippedGun == 2) {
				shotGunBullets += 10;
				currentFireRate = shotGunFireRate;
			}

			//Debug.Log("new fire rate!");
		}

		
}
