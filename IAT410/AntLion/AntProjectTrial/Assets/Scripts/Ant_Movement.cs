﻿using UnityEngine;
using System.Collections;

public class Ant_Movement : MonoBehaviour {

	public GameManager gameManager;
	public float speed;
	Animator anim;
	float damaged = 0.2f;

    public bool Shield = false;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
    }


    void Movement() {
		anim.SetFloat ("speed", Mathf.Abs (Input.GetAxis ("Horizontal")));
		anim.SetFloat ("speed", Mathf.Abs (Input.GetAxis ("Vertical")));

		if (Input.GetAxisRaw ("Horizontal") > 0) 
		{
						transform.Translate(Vector3.right * speed * Time.deltaTime);
						transform.eulerAngles = new Vector2(0,0);
		}
		if (Input.GetAxisRaw ("Horizontal") < 0) 
		{
						transform.Translate(-Vector3.right * speed * Time.deltaTime);
						//transform.eulerAngles = new Vector2(0,180);
		}

		if (Input.GetAxisRaw ("Vertical") > 0) 
		{
						transform.Translate(Vector3.up * speed * Time.deltaTime);
						transform.eulerAngles = new Vector2(0,0);
		}
		if (Input.GetAxisRaw ("Vertical") < 0) 
		{
						transform.Translate(-Vector3.up * speed * Time.deltaTime);	
						//transform.eulerAngles = new Vector2(0,180);
		}




		
	}

	
	/*public IEnumerator Damaged(){
		// flash when hurt
		GetComponent<Renderer>().enabled = false;
		yield return new WaitForSeconds(damaged);
		GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(damaged);
		GetComponent<Renderer>().enabled = false;
		yield return new WaitForSeconds(damaged);
		GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(damaged);
		GetComponent<Renderer>().enabled = false;
		yield return new WaitForSeconds(damaged);

	}*/


		



	void OnCollisionEnter(Collision collision) {



		if (collision.gameObject.tag == "Enemy") 
		{


			Destroy(collision.gameObject);
			//Application.LoadLevel("GameOver");
			if(Shield == true) {
				Shield = false;
				return;
				
			}
			
			
		}

		if (collision.gameObject.tag == "Shield") 
		{
			Debug.Log ("Shield Activated");
			Shield = true;
			Destroy(collision.gameObject);

				
		}
	}
    void FixedUpdate()
   
    {
        Movement();
        print(Input.mousePosition.x);
        
       
    }
    
    void Update()
    {
    }

}
