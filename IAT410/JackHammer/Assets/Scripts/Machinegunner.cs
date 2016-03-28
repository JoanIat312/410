using UnityEngine;
using System.Collections;

public class Machinegunner : MonoBehaviour {
	public Transform target;
	public float zOffset;
	public float xOffset; 
	//public int health;
	//public GameManager gameManager;// need this but dont know why
	public NavMeshAgent myAgent;
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
      Vector3 objectPos = transform.position;
      Vector3 targetPos = GameObject.Find("Player").transform.position;
      float angle = Mathf.Atan2(targetPos.z - objectPos.z, targetPos.x - objectPos.x) * Mathf.Rad2Deg/*180/3.14159265f*/;
      if (angle < 0)
      {
       angle += 360;
      }
//      Debug.Log(angle);
//    degrees = 0 + (degrees - 30) * (8 - 0) / (390 - 30);

  animator.SetFloat("Direction", angle);
	}

	void LateUpdate() {
		transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
	}

	IEnumerator TakeDamage() {
		// flash enemy when hit
		GetComponent<SpriteRenderer> ().color = new Color (255f, 0f, 0f);
		yield return new WaitForSeconds(0.1f); 
		GetComponent<SpriteRenderer> ().color = new Color (255f, 255f, 255f);
	}

     float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n){
      // angle in [0,180]
      float angle = Vector3.Angle(a,b);
      float sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));

      // angle in [-179,180]
      float signed_angle = angle * sign;

      // angle in [0,360] (not used but included here for completeness)
      //float angle360 =  (signed_angle + 180) % 360;

      return signed_angle;
     }

}
