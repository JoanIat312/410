using UnityEngine;
using System.Collections;

public class Enemy_Default_Behaviour : MonoBehaviour {
	private Vector3 Player;
	private Vector3 Playerdirection;
	private float xDif;
	private float yDif;
	public float speed;
	private Rigidbody rb;
	private bool hit = false;
	public int health;
    public GameManager gameManager;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		speed = 3;
		health = 100;
	}
//	// Update is called once per frame
//	void Update () {
//		if (Input.GetKeyDown ("space")) {
//			rb.velocity = new Vector3 (0, 0, 1);
//		} 
//	}
	// Update is called once per frame
	void FixedUpdate () {
		//if (Input.GetKeyDown ("space")) {
				if (GameManager.stunEnemies == true) {
				rb.velocity = new Vector3 (0, 0, 1);
				//Debug.Log ("SPACE!");
			}
		//}
		else if (GameManager.stunEnemies == false) 
		{

			Player = GameObject.Find ("Player").transform.position;
			xDif = Player.x - transform.position.x;
			yDif = Player.y - transform.position.y;

			Playerdirection = new Vector3 (xDif, yDif, 1);
			rb.velocity = (Playerdirection.normalized * speed);
			if (hit == true) {
					rb.velocity = -rb.velocity;
					Invoke ("TurnAround", 1);
			}
		}
        if (health <= 0)
        {
            destory();
        }
    }

	void OnCollisionEnter(Collision collision) {
        //Debug.Log("HIT WALL - ROTATING!"); // Display it in UI
        if (hit == true)
        {
            hit= false;
        }
        else if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "Enemy") 
    		{
                //Vector3 eulerAngles = transform.rotation.eulerAngles;          

                // Set the altered rotation back
                //transform.rotation = Quaternion.AngleAxis(180, transform.forward) * transform.rotation;
                hit = true;
    		}
    }

	void TurnAround() {
		rb.velocity = -rb.velocity;
	}

	IEnumerator TakeDamage(int damage) {
		// flash enemy when hit
		GetComponent<SpriteRenderer> ().color = new Color (255f, 0f, 0f);
		yield return new WaitForSeconds(0.1f); 
		GetComponent<SpriteRenderer> ().color = new Color (255f, 255f, 255f);
        health -= damage;
        Debug.Log(health);
    }
    void destory()
    {
        gameManager.SendMessage("ScoreTracker", SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }
}

