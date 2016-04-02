using UnityEngine;
using System.Collections;

public class Cannonballs : MonoBehaviour
{
 

	// Use this for initialization
	public AudioClip explosion;
	public int moveSpeed = 0;
	private Vector3 objectPos;
	private Vector3 targetPos;
	private Vector3 dis;
	private Quaternion num;
	private float angle;
	private Rigidbody rb;
	private bool hitWall;
	public int defaultDamage;
	Animator anim;
	public GameManager gameManager;

	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		hitWall = false;
		defaultDamage = 20;
		objectPos = transform.position;
		targetPos = GameObject.Find ("Player").transform.position;
		//Vector3 zConvertedObjectPos = new Vector3(objectPos.x, 1, objectPos.y);
		//Vector3 zConvertedTargetPos = new Vector3(targetPos.x, 1, targetPos.y);

		//dis = zConvertedTargetPos - zConvertedObjectPos;
		dis = targetPos - objectPos;
		dis.Normalize ();

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "wall" && gameObject.name != "cannonBalls") {
			hitWall = true;
			anim.SetBool ("distroy", true);
			anim.Play ("cannonExplosion", 0, 0);
			AudioSource.PlayClipAtPoint (explosion, transform.position);         
			Destroy (gameObject, .8f);
		}
		if (col.gameObject.tag == "Player" && gameObject.name != "cannonBalls") {
			col.gameObject.SendMessage ("Damaged", SendMessageOptions.DontRequireReceiver);
			gameManager.SendMessage ("PlayerDamage", defaultDamage, SendMessageOptions.DontRequireReceiver);
			Destroy (gameObject);
		}
	}

	void FixedUpdate ()
	{

		if (gameObject.name != "cannonBalls") {
			if (hitWall == true) {
				rb.velocity = new Vector3 (0, 0, 0);   
			} else {
				rb.velocity = (dis * moveSpeed);
			}
		}

	}


}
