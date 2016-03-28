using UnityEngine;
using System.Collections;

public class Rockets : MonoBehaviour {

	public AudioClip explosion;
	public int moveSpeed;
	private Vector3 objectPos;
	private Vector3 targetPos;
	private Vector3 dis;
	private Quaternion num;
	private float angle;
	private Rigidbody rb;
	private bool hitWall;
	public int defaultDamage;
	Animator anim;
	float chaseTimer = 5f;
	public GameManager gameManager;
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		hitWall = false;
		defaultDamage = 20;
		moveSpeed = 5;
		objectPos = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

			targetPos = GameObject.Find ("Player").transform.position;
			float xDif = targetPos.x - transform.position.x;
			float zDif = targetPos.z - transform.position.z;
			if (gameObject.name != "Rockets") {
				if (hitWall == true) {
					rb.velocity = new Vector3 (0, 0, 0);   
				} else {
					rb.velocity = (dis.normalized * moveSpeed);
				}
			}
			dis = new Vector3 (xDif, 0.38f, zDif);

		//dis = targetPos - objectPos;
		//dis.Normalize ();
		//rb.velocity = (targetPos.normalized * moveSpeed);

		//rb.velocity = (dis.normalized * moveSpeed);

	}

	void FixedUpdate ()
	{
		
		/*if (gameObject.name != "Rockets") {
			if (hitWall == true) {
				rb.velocity = new Vector3 (0, 0, 0);   
			} else {
				rb.velocity = (dis.normalized * moveSpeed);
			}
		}*/

	}

	void OnTriggerEnter (Collider col)
	{
		
		if (col.gameObject.tag == "wall" && gameObject.name != "Rockets") {
			hitWall = true;
			anim.SetBool ("distroy", true);
			anim.Play ("cannonExplosion", 0, 0);
			AudioSource.PlayClipAtPoint (explosion, transform.position);         
			Destroy (gameObject, 0.5f);
		}
		if (col.gameObject.name == "Player" && gameObject.name != "Rockets"){
			col.gameObject.SendMessage ("Damaged", SendMessageOptions.DontRequireReceiver);
			gameManager.SendMessage ("PlayerDamage", defaultDamage, SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
	}
		

}
