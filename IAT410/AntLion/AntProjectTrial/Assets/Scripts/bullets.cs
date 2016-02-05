using UnityEngine;
using System.Collections;

public class bullets : MonoBehaviour {

	// Use this for initialization
	public int moveSpeed = 2 ;
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () { 
	}

	void OnTriggerEnter(Collider col){

			if (col.gameObject.tag == "Bottom") 
			{

					Destroy(gameObject);


			}
	}
    void FixedUpdate()
    {
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dis = Input.mousePosition - objectPos;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg));
        transform.position += new Vector3(1,1,0) * moveSpeed * Time.deltaTime;
    }
}
