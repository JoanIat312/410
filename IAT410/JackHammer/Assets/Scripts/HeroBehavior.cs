using UnityEngine;
using System.Collections;

public class HeroBehavior : MonoBehaviour
{

	public NavMeshAgent myAgent;
	public Transform target;
	private GameObject player;
	private Vector3 playerPos;
	public GameManager gameManager;
	private Animator anim;
	private bool rescued = false;
    void Start()
    {
        
		anim = GetComponent<Animator>();
        //rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		if (myAgent.velocity.x == 0 && myAgent.velocity.z == 0)
		{
			anim.SetInteger("Direction", 0);
		}
		else if (myAgent.velocity.z > .5f) { // up
			anim.SetInteger("Direction", 1);
		}
		else if (myAgent.velocity.z < -.5f) { // down
			anim.SetInteger("Direction", 3);
		}
		else if (myAgent.velocity.x > .5f) { // right
			anim.SetInteger("Direction", 2);
		}
		else if (myAgent.velocity.x < -.5f) { // left
			anim.SetInteger("Direction", 4);
		}
		transform.position = new Vector3(target.position.x, 0.38f, target.position.z);
    }
    
	void burgerSpawn(){
		anim.SetBool ("burger", true);

	}

	IEnumerator burgerStop(){
		Debug.Log ("enter");
		yield return new WaitForSeconds(9f); 
		anim.SetBool ("burger", false);
	}
}