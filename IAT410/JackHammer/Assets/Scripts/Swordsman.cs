using UnityEngine;
using System.Collections;

public class Swordsman : MonoBehaviour {
    
     public Transform target;
     public float zOffset;
     public float xOffset; 
     public int health;
     public GameManager gameManager;// need this but dont know why
	// Use this for initialization
	void Start () {
	   health = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
