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
        }
		if (Input.GetAxisRaw ("Horizontal") < 0) 
		{
            transform.Translate(-Vector3.right * speed * Time.deltaTime);
           //transform.eulerAngles = new Vector2(0,180);
        }

		if (Input.GetAxisRaw ("Vertical") > 0) 
		{
			transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
		if (Input.GetAxisRaw ("Vertical") < 0) 
		{
			transform.Translate(-Vector3.up  * speed * Time.deltaTime);
        }
	}

	
	IEnumerator Damaged(){
        GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f);

    }


		



   void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.tag == "bullets") 
		{
            
            StartCoroutine(Damaged());

            if(GameManager.shield == true)
            {
                gameManager.SendMessage("PlayerDamage", 5, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                gameManager.SendMessage("PlayerDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
        }
		if (collision.gameObject.tag == "Finish") {
				gameManager.SendMessage ("loadNextScene", SendMessageOptions.DontRequireReceiver);
		}
	}

     void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == "Enemy")
        {
           StartCoroutine(Damaged());
           if(GameManager.shield == true)
           {
            gameManager.SendMessage("PlayerDamage", .1f, SendMessageOptions.DontRequireReceiver);
           }
           else
           {
            gameManager.SendMessage("PlayerDamage", .5f, SendMessageOptions.DontRequireReceiver);
           }
        }
     }

    void FixedUpdate()
   
    {
        Movement();
        if (Input.GetKey(KeyCode.A))
        {
            //anim.SetBool("left", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //anim.SetBool("left", false);
        }
        
       
    }
    
    void Update()
    {
    }

}
