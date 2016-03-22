using UnityEngine;
using System.Collections;

public class TankShotGunBullets : MonoBehaviour {

	public GameManager gameManager;
	public AudioClip explosion;
	public int moveSpeed = 5;
	private Vector3 objectPos;
	private Vector3 targetPos;
	private Vector3 dis;
	private Quaternion num;
	private float angle;
	private Rigidbody rb;
	private bool hitWall;
	public int defaultDamage;
	Animator anim;

	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		hitWall = false;
		defaultDamage = 10;

		objectPos = transform.position;
		targetPos = GameObject.Find ("Player").transform.position;


		dis = targetPos - objectPos;
		dis = dis + new Vector3 (Random.Range (-5, 5), 0, Random.Range (-5, 5));
		dis.Normalize ();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "wall" && gameObject.name != "TankShotGunBullets") { // dont destroy the original bullet, only the clones
			hitWall = true;
			anim.Play ("bulletExplosion", 0, 0);
			AudioSource.PlayClipAtPoint (explosion, transform.position);          
			Destroy (gameObject, .4f);
		}
		if(col.gameObject.tag == "Player" && gameObject.name != "TankShotGunBullets"){
			col.gameObject.SendMessage ("Damaged", SendMessageOptions.DontRequireReceiver);
			gameManager.SendMessage ("PlayerDamage", defaultDamage, SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
	}

	void FixedUpdate ()
	{

		if (gameObject.name != "TankShotGunBullets") {
			if (hitWall == true) {
				rb.velocity = new Vector3 (0, 0, 0);    
			} else {
				rb.velocity = (dis * moveSpeed);
			}
		}

	}
}
