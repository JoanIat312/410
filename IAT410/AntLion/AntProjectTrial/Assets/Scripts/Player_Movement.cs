using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

	public GameManager gameManager;
	public float speed;
	Animator anim;
    private int damage = 30;
	float damaged = 0.2f;
	Rigidbody rb;

    public bool Shield = false;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = gameObject.GetComponent<Rigidbody> ();
       
    }


    void Movement() {
		
		

		if (Input.GetAxisRaw ("Horizontal") > 0) 
		{
            transform.Translate(Vector3.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0,0);
            anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        }
		if (Input.GetAxisRaw ("Horizontal") < 0) 
		{
            transform.Translate(-Vector3.right * speed * Time.deltaTime);
            //transform.eulerAngles = new Vector2(0,180);
            anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        }

		if (Input.GetAxisRaw ("Vertical") > 0) 
		{
						transform.Translate(Vector3.up * speed * Time.deltaTime);
						transform.eulerAngles = new Vector2(0,0);
            anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Vertical")));
        }
		if (Input.GetAxisRaw ("Vertical") < 0) 
		{
						transform.Translate(-Vector3.up * speed * Time.deltaTime);
            //transform.eulerAngles = new Vector2(0,180);
            anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Vertical")));
        }




		
	}

	
	public IEnumerator Damaged(){
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

	}


		



	void OnCollisionEnter(Collision collision) {



		if (collision.gameObject.tag == "Enemy") 
		{

            Damaged();
			Destroy(collision.gameObject);
			rb.velocity = new Vector3(0, 0, 0); // make the player stop moving after getting hit

            gameManager.SendMessage("PlayerDamage", damage, SendMessageOptions.DontRequireReceiver);

			
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
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("left", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("left", false);
        }
        
       
    }
    
    void Update()
    {
    }

}
