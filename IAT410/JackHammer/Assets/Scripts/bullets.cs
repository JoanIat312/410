using UnityEngine;
using System.Collections;

public class bullets : MonoBehaviour
{

	// Use this for initialization
    public AudioClip explosion;
	public int moveSpeed = 5;
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
		defaultDamage = 25;
  
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 zConvertedObjectPos = new Vector3(objectPos.x, 0, objectPos.y);
        Vector3 zConvertedMousePos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);

//        Debug.Log("zConvertedObjectPos: " + zConvertedObjectPos);
//        Debug.Log("zConvertedMousePos: " + zConvertedMousePos);
        dis = zConvertedMousePos - zConvertedObjectPos;
        dis.Normalize();
    }

	// Update is called once per frame
	void Update ()
	{
        
    }

	void OnTriggerEnter (Collider col)

	{
        if (col.gameObject.tag == "wall" && gameObject.name != "bullets")
        {
			hitWall = true;
			anim.Play("bulletExplosion", 0, 0);
			 AudioSource.PlayClipAtPoint(explosion, transform.position);			
            Destroy(gameObject, .4f);
        }
        if (col.gameObject.tag == "Enemy" && gameObject.name != "bullets")
        {
			col.gameObject.SendMessage("TakeDamage", defaultDamage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
 
    void FixedUpdate ()
	{

        if (gameObject.name != "bullets")
        {
            if (hitWall == true) {
				rb.velocity = new Vector3 (0, 0, 0);	
			} else {
                rb.velocity = (dis * moveSpeed);
			}
        }
       
	}


}
