using UnityEngine;
using System.Collections;

public class bullets : MonoBehaviour
{

	// Use this for initialization
	public int moveSpeed = 20;
	private Vector3 objectPos;
	private Vector3 dis;
	private Quaternion num;
	private float angle;
	private Rigidbody rb;
	private bool hitWall;
	public int defaultDamage;
    Animator anim;

	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
        anim = GetComponent<Animator>();
		hitWall = false;
		defaultDamage = 50;
        //http://answers.unity3d.com/questions/736511/shoot-towards-mouse-in-unity2d.html
        /*angle = Mathf.Atan2 (dis.y, dis.x) * Mathf.Rad2Deg;

        if (angle < 0) {
                angle += 360;
        }

        num = Quaternion.Euler (new Vector3 (0, 0, angle));*/
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        dis = Input.mousePosition - objectPos;
        dis.Normalize();
    }

	// Update is called once per frame
	void Update ()
	{

        //transform.position += new Vector3(0.5f, 0, 0);
        //Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }

	void OnTriggerEnter (Collider col)

	{
        if (col.gameObject.tag == "wall" && gameObject.name != "bullets")
        {
			hitWall = true;
			anim.Play("bulletExplosion", 0, 0);
											
            Destroy(gameObject, .4f);
            
            
        }
        if (col.gameObject.tag == "Enemy" && gameObject.name != "bullets")
        {
			col.gameObject.SendMessage("TakeDamage", defaultDamage, SendMessageOptions.DontRequireReceiver);
            //Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
 
    void FixedUpdate ()
	{

      /*          if (num.z > 0.5f)
                {
                    num = Quaternion.Euler(num.x, num.y, 0.5f);
                }
        
                if (num.z < -0.5f)
                {
                    num = Quaternion.Euler(num.x, num.y, -0.5f);
                }

        //transform.position += new Vector3(1, angle, 0);*/
        if (gameObject.name == "bullets")
        {

//			if (hitWall == true) {
//
//				rb.velocity = new Vector3 (0, 0, 0);	
//			} else {
//								Debug.Log ("poo");
//
//				rb.velocity += Vector3.down;
//
//			}
        }
        else {
			if (hitWall == true) {
				rb.velocity = new Vector3 (0, 0, 0);	
			} else {
//				rb.velocity += Vector3.down;
				rb.velocity = (dis * moveSpeed);

			}
        }
	}


}
