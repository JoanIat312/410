using UnityEngine;
using System.Collections;

public class Swordsman : MonoBehaviour {
    
     public Transform target;
     public float zOffset;
     public float xOffset; 
     //public int health;
     public GameManager gameManager;// need this but dont know why
    public NavMeshAgent myAgent;
    private Animator anim;

	// Use this for initialization
	void Start () {
	   //health = 100;
       anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
          if (myAgent.velocity.x == 0 && myAgent.velocity.z == 0)
          {
             anim.SetInteger("Direction", 0);
          }
          else if (myAgent.velocity.x > .3f) { // right
           anim.SetInteger("Direction", 2);
          }
          else if (myAgent.velocity.x < -.3f) { // left
           anim.SetInteger("Direction", 4);
          }
          else if (myAgent.velocity.z > .3f) { // up
           anim.SetInteger("Direction", 1);
          }
          else if (myAgent.velocity.z < -.3f) { // down
           anim.SetInteger("Direction", 3);
          }

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


	IEnumerator Stunned() {
		// flash enemy when hit
		GetComponent<SpriteRenderer> ().color = new Color (0f, 213f, 244f);
		yield return new WaitForSeconds(0.8f); 
		GetComponent<SpriteRenderer> ().color = new Color (255f, 255f, 255f);
	}
}
