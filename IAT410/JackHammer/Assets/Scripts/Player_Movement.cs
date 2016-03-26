using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour
{
	public AudioClip checkpoint;
	public GameManager gameManager;
	public GameObject bloodSpawner;
	public float speed;
	Animator anim;
	private int damage = 20;
	float damaged = 0.1f;
	Rigidbody rb;
	bool previousFrameStun = false;
	bool stunning = false;

	public bool Shield = false;


	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		rb = gameObject.GetComponent<Rigidbody> ();
	}


	void Movement ()
	{
		bool inputPressed = false;
		if (Input.GetAxisRaw ("Horizontal") > 0) {
			inputPressed = true;
			transform.Translate (Vector3.right * speed * Time.deltaTime);
			//transform.eulerAngles = new Vector2(0,0);
		}
		if (Input.GetAxisRaw ("Horizontal") < 0) {
			transform.Translate (-Vector3.right * speed * Time.deltaTime);
			inputPressed = true;
			//transform.eulerAngles = new Vector2(0,180);
		}
		if (Input.GetAxisRaw ("Vertical") > 0) {
			transform.Translate (Vector3.up * speed * Time.deltaTime);
			inputPressed = true;
		}
		if (Input.GetAxisRaw ("Vertical") < 0) {
			transform.Translate (-Vector3.up * speed * Time.deltaTime);
			inputPressed = true;
		}
		anim.SetBool ("Moving", inputPressed);
	}

	
	IEnumerator Damaged ()
	{
		bloodSpawner.SendMessage ("spawn", transform.position, SendMessageOptions.DontRequireReceiver);
		GetComponent<SpriteRenderer> ().color = new Color (255f, 0f, 0f);
		yield return new WaitForSeconds (0.1f);
		GetComponent<SpriteRenderer> ().color = new Color (255f, 255f, 255f);

	}

	void OnCollisionEnter (Collision collision)
	{

		if (collision.gameObject.tag == "bullets") {
            
			StartCoroutine (Damaged ());

			if (GameManager.shield == true) {
				
				gameManager.SendMessage ("PlayerDamage", 5, SendMessageOptions.DontRequireReceiver);
			} else {
				gameManager.SendMessage ("PlayerDamage", damage, SendMessageOptions.DontRequireReceiver);
			}
		}
		if (collision.gameObject.tag == "Finish") {
			gameManager.SendMessage ("loadNextScene", SendMessageOptions.DontRequireReceiver);
			AudioSource.PlayClipAtPoint (checkpoint, transform.position);
		}
	}

	void OnCollisionStay (Collision collision)
	{
		if (collision.gameObject.tag == "EnemyAgent") {
			StartCoroutine (Damaged ());
			if (GameManager.shield == true) {
				gameManager.SendMessage ("PlayerDamage", .1, SendMessageOptions.DontRequireReceiver);
			} else {
				gameManager.SendMessage ("PlayerDamage", 1, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	void FixedUpdate ()
	{
		if (stunning == false) {
			Movement ();
		}

	}
	//     static float Remap (this float value, float from1, float to1, float from2, float to2) {
	//      return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	//     }

	void Update ()
	{
		// get angle to select sprite
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 dis = Input.mousePosition - objectPos;
		Quaternion num = Quaternion.Euler (new Vector3 (0, 0, Mathf.Atan2 (dis.y, dis.x) * Mathf.Rad2Deg));
		float degrees = num.eulerAngles.z + 30;
		//  degrees = Remap(degrees, 0, 360, 0, 40);
		degrees = 0 + (degrees - 30) * (8 - 0) / (390 - 30);
//      low2 + (value - low1) * (high2 - low2) / (high1 - low1)

		//Debug.Log(degrees);
		anim.SetFloat ("Direction", degrees);

		/*
		if ( (Time.time > gameManager.stunUseDelayTimeStamp) && Input.GetKeyDown (KeyCode.Space)) { 
			
			StartCoroutine (stunAnimation());
		}*/
		//Time.time >= gameManager.stunUseDelayTimeStamp
		Debug.Log( (Time.time > gameManager.stunUseDelayTimeStamp) + ", " + Input.GetKeyDown (KeyCode.Space));
		if (Time.time >= gameManager.stunUseDelayTimeStamp) {
			
			if (Input.GetKeyDown (KeyCode.Space)) {
				
				StartCoroutine (stunAnimation());
			}
		}
	}

	IEnumerator stunAnimation ()
	{
		stunning = true;
      	Debug.Log("SHIT!");
		anim.SetBool ("stun", true);
		yield return new WaitForSeconds (.5f); // wait for two seconds.
		anim.SetBool ("stun", false);
		stunning = false;
	}
}