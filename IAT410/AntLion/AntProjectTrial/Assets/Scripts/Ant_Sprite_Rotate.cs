using UnityEngine;
using System.Collections;

public class Ant_Sprite_Rotate : MonoBehaviour {
		//Animator anim;

		// Use this for initialization
		void Start () {
				//anim = GetComponent<Animator> ();
		}

		// Update is called once per frame
		void Update () {
				LookAtMouse ();
		}

		void LookAtMouse() {
				var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				Quaternion rot = Quaternion.LookRotation (mousePosition - transform.position, transform.TransformDirection(Vector3.forward));
				transform.localRotation = new Quaternion(0, 0, rot.z, rot.w);
		}

}
