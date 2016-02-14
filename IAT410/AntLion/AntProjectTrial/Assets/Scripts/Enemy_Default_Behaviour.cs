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

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		speed = 3;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Player = GameObject.Find ("Ant_Player").transform.position;
		xDif = Player.x - transform.position.x;
		yDif = Player.y - transform.position.y;

		Playerdirection = new Vector3 (xDif, yDif, 1);
		rb.velocity = (Playerdirection.normalized * speed);
        if(hit == true)
        {
            rb.velocity = -rb.velocity;
        }
	}

	void OnCollisionEnter(Collision collision) {
        Debug.Log("HIT WALL - ROTATING!"); // Display it in UI
        if (hit == true)
        {
            hit= false;
        }else if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "Enemy") 
		{
            //Vector3 eulerAngles = transform.rotation.eulerAngles;          

            // Set the altered rotation back
            //transform.rotation = Quaternion.AngleAxis(180, transform.forward) * transform.rotation;
            hit = true;
		}
        
    }
}
