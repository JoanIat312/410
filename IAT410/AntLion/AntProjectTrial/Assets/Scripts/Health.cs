using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public Texture playersHealthTexture;
	public float screenPositionX;
	public float screenPositionY;

	//public int iconSizeX = 25;
	//public int iconSizeY = 25;

	public int playersHealth = 100;

	public GUIStyle Health_bar_GUI;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (playersHealth > 100) {

			playersHealth = 100;
				}
		if (playersHealth < 0) {

			playersHealth = 0;

				}
	
	}

	void OnGUI() {

		GUI.Box (new Rect (5, 5, Screen.width / 3 / (100 / playersHealth), 20), "" + playersHealth, Health_bar_GUI);
	}

}
