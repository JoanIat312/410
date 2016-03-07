using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

	public GameManager gameManager;
	public float speed;
	Animator anim;
    private int damage = 20;
	float damaged = 0.1f;
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
			//transform.eulerAngles = new Vector2(0,0);
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
            anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Vertical")));
        }
		if (Input.GetAxisRaw ("Vertical") < 0) 
		{
			transform.Translate(-Vector3.up  * speed * Time.deltaTime);
            anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Vertical")));
        }




		
	}

	
	IEnumerator Damaged(){
        GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f);

    }


		



	void OnCollisionEnter(Collision collision) {



		if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "bullets") 
		{
            
            StartCoroutine(Damaged());
            //Destroy(collision.gameObject);
            //rb.velocity = new Vector3(0, 0, 0); // make the player stop moving after getting hit
            if(GameManager.shield == true)
            {
                gameManager.SendMessage("PlayerDamage", 5, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                gameManager.SendMessage("PlayerDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
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
