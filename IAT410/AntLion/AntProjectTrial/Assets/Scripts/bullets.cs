using UnityEngine;
using System.Collections;

public class bullets : MonoBehaviour {

	// Use this for initialization
	public int moveSpeed = 20 ;

	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
        /*if(!r.isVisible){
            Destroy(gameObject);
        }*/
        //transform.position += new Vector3(0.5f, 0, 0);

        if (transform.position.x >= 100 && gameObject.name == "bullets(Clone)")
        {
           Destroy(gameObject);
        }
    }

	void OnTriggerEnter(Collider col){
	}
    void FixedUpdate()
    {

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dis = Input.mousePosition - objectPos;
        Quaternion num = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg));
        if (num.z > 0.5f)
        {
            num = Quaternion.Euler(num.x, num.y, 0.5f);
        }

        if (num.z < -0.5f)
        {
            num = Quaternion.Euler(num.x, num.y, -0.5f);
        }

        transform.position += new Vector3(1, num.z, 0);
        
    }
}
