
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
public float time = 20;
public float countingDown = 0.01f;
public int playerHealth = 100;
public Ant_Movement movement;
public GUIStyle Health_bar_GUI;
public Texture playersHealthTexture;

	void OnGUI() {
		
		if (playerHealth > 0 && playerHealth <= 100){
			
			GUI.Box (new Rect (0, 5, Screen.width / 3 / (100 / playerHealth), 20), "" + playerHealth, Health_bar_GUI);
			GUI.Box (new Rect (0, 30, Screen.width / 3 / (20 / time), 20), "" + time, Health_bar_GUI);
		}
	}

	void PlayerHealthPlus(int health){
		if(playerHealth < 100){
			playerHealth += health;
		}
		if(playerHealth >100){
			playerHealth = 100;
		}
		if(playerHealth <= 0){
			playerHealth = 0;
		}
		
	}
	void TimeIncrease(float t){
		if (time >= 0) {
			time -= t;
		}
		
	}
	void TimeDecrease(float t){
		if (time >= 0) {
			time += t;
		}
		
	}

	void PlayerDamage(int damage){
		if(playerHealth > 0){
			playerHealth -= damage;
		}
		if(playerHealth <= 0){
			playerHealth = 0;
			//Application.LoadLevel("GameOver");
			//Restart();
		}
		
	}
	void Update(){
		time -= countingDown;
		if(time <= 0){
			time = 0;
		}
		
	}
	IEnumerator PlayerFreeze(float t){
		float s = movement.speed;
		movement.speed = 0;
		yield return new WaitForSeconds (t);
		movement.speed = s;

	}

	
}
