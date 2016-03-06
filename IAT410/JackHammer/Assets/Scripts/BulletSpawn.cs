using UnityEngine;
using System.Collections;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bObject;
    public AudioClip shot;
    public int equippedGun = 0; // active weapon status - 0 is default, 1 is fast shootng machinegun
    private float currentFireRate = .1f;

    public float defaultFireRate = .1f;
    public float machineGunFireRate = .2f;

    public int machineGunBullets = 50;

    private float nextBulletSpawnTimestamp;
    // Use this for initialization
    void Start()
    {
        transform.position = GameObject.Find("jackhammer-gun").transform.position;
		//fireRate = .5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Update()
    {

       
    if ((Input.GetMouseButton(0)) && (Time.time >= nextBulletSpawnTimestamp))
        {
            Debug.Log("left pressed");
            Spawn();
        }
    }

    void Spawn()
    {
        nextBulletSpawnTimestamp = Time.time + currentFireRate;
        if ((equippedGun == 1) && (machineGunBullets > 0))
        {
           machineGunBullets -= 1;
           Debug.Log(machineGunBullets);
           if (machineGunBullets <= 0)
           {
                // go back to default gun
                SetWeapon(0);
           }
        }

        GameObject newBullet = Instantiate(bObject, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation) as GameObject;
        AudioSource.PlayClipAtPoint(shot, transform.position);
        newBullet.tag = "bullets";
        Debug.Log("new bullet created");

    }

     public void SetWeapon(int newWeapon) {
      this.equippedGun = newWeapon;

      if (equippedGun == 0)
      {
       currentFireRate = defaultFireRate;
      }
      else if (equippedGun == 1)
      {
        machineGunBullets = 75;
        currentFireRate = machineGunFireRate;
      }

      //Debug.Log("new fire rate!");
     }
}