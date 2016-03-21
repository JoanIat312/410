using UnityEngine;
using System.Collections;

public class ShotgunBullet : MonoBehaviour
{

	// Use this for initialization
	public GameManager gameManager;
	public AudioClip explosion;
	public int moveSpeed = 5;
	private Vector3 objectPos;
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
		defaultDamage = 25;

		objectPos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 zConvertedObjectPos = new Vector3 (objectPos.x, 0, objectPos.y);
		Vector3 zConvertedMousePos = new Vector3 (Input.mousePosition.x, 0, Input.mousePosition.y);

//  Debug.Log("zConvertedObjectPos: " + zConvertedObjectPos);
//  Debug.Log("zConvertedMousePos: " + zConvertedMousePos);

		dis = zConvertedMousePos - zConvertedObjectPos;
//  Debug.Log("dis before: " + dis);
		dis = dis + new Vector3 (Random.Range (-70, 70), 0, Random.Range (-70, 70));
//  Debug.Log("dis after: " + dis);
		dis.Normalize ();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "wall" && gameObject.name != "shotgunBullets") { // dont destroy the original bullet, only the clones
			hitWall = true;
			anim.Play ("bulletExplosion", 0, 0);
			AudioSource.PlayClipAtPoint (explosion, transform.position);          
			Destroy (gameObject, .4f);
		}
		if (col.gameObject.tag == "Enemy" && gameObject.name != "shotgunBullets") {
			col.gameObject.SendMessage ("TakeDamage", defaultDamage, SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
		if(col.gameObject.tag == "Player" && gameObject.name != "shotgunBullets"){
			gameManager.SendMessage ("PlayerDamange", defaultDamage, SendMessageOptions.DontRequireReceiver);
		}
	}

	void FixedUpdate ()
	{

		if (gameObject.name != "shotgunBullets") {
			if (hitWall == true) {
				rb.velocity = new Vector3 (0, 0, 0);    
			} else {
				rb.velocity = (dis * moveSpeed);
			}
		}

	}


}
