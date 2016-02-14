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

	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		
		//http://answers.unity3d.com/questions/736511/shoot-towards-mouse-in-unity2d.html
				/*angle = Mathf.Atan2 (dis.y, dis.x) * Mathf.Rad2Deg;

				if (angle < 0) {
						angle += 360;
				}

				num = Quaternion.Euler (new Vector3 (0, 0, angle));*/
	}

	// Update is called once per frame
	void Update ()
	{

        //transform.position += new Vector3(0.5f, 0, 0);
        //Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        dis = Input.mousePosition - objectPos;
        dis.Normalize();
    }

	void OnTriggerEnter (Collider col)

	{
        if (col.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
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
            rb.velocity += Vector3.down;
        }
        else {
            rb.velocity = (dis * moveSpeed);
        }
	}
}
