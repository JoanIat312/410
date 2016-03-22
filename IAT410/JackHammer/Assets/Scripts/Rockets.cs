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
	public GameManager gameManager;
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		hitWall = false;
		defaultDamage = 20;
		moveSpeed = 10;
		objectPos = transform.position;

	
	}
	
	// Update is called once per frame
	void Update () {
		targetPos = GameObject.Find ("Player").transform.position;
		dis = targetPos - objectPos;
		dis.Normalize ();
		//rb.velocity = (targetPos.normalized * moveSpeed);
		transform.Translate (dis+Vector3.left);
	
	}

	/*void FixedUpdate ()
	{

		if (gameObject.name != "Rockets") {
			if (hitWall == true) {
				rb.velocity = new Vector3 (0, 0, 0);   
			} else {
				rb.velocity = (dis * moveSpeed);
			}
		}

	}*/
}
