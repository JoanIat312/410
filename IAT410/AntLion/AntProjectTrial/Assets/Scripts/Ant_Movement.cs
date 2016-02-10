using UnityEngine;
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
        anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        //Debug.Log(Mathf.Abs(Input.GetAxis("Horizontal")));

		//anim.SetFloat ("speed", Mathf.Abs (Input.GetAxis ("Vertical")));
		
		if (Input.GetKey(KeyCode.W)) 
		{
           
            if (true)
            {
           
                transform.position += (new Vector3( 0, 1 * speed, 0) * Time.deltaTime);
				//transform.eulerAngles = new Vector2(0,0);
			}
		}
		if (Input.GetKey(KeyCode.S)) 
		{
            
            if (true)
            {
                transform.position += (new Vector3(0, -1 * speed, 0) * Time.deltaTime);
                //transform.eulerAngles = new Vector2(0,180);
            }
		}

		if (Input.GetKey(KeyCode.A)) 
		{
            anim.SetBool("left", true);
            if (true){
                transform.position += (new Vector3(-1 * speed, 0, 0) * Time.deltaTime);
                //transform.eulerAngles = new Vector2(0,0);
            }
		}
		if (Input.GetKey(KeyCode.D)) 
		{
            anim.SetBool("left", false);
            if (true)
            {
				transform.position += (new Vector3(1 * speed, 0, 0) * Time.deltaTime);	
			//transform.eulerAngles = new Vector2(0,180);
			}
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
