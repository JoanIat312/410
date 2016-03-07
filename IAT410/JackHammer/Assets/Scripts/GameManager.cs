
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public int playerHealth = 100;
    //public Player_Movement movement;
    public GUIStyle Health_bar_GUI;
    public Camera main;
    private GameObject player;
    private BulletSpawn playerBulletSpawner; // used to get number of bullets remaining

	public static bool stunEnemies;
    public static int score = 0;

	private float stunDurationTimeStamp;
	public static float stunDuration = 3f;
	public static float stunUseDelay = 10f;
	private float stunUseDelayTimeStamp;

    public static bool shield = false;


    public float time = 5;
    public float countDown = 0.0f;
    
    void Start() {
        playerBulletSpawner = GameObject.Find("PlayerBulletSpawner").GetComponent<BulletSpawn>();
    }

	void OnGUI() {

        if (playerHealth > 0 && playerHealth <= 100 && Application.loadedLevel != 1)
        {
            GUI.Box(new Rect(10, 30, Screen.width / 3 / (100 / playerHealth), 20), "Health: " + playerHealth, Health_bar_GUI);

            if (shield == true)
            {
                GUI.Box(new Rect(Screen.width / 3 / (100 / playerHealth) + 20, 30, Screen.width / 3 / (10 / time), 20), "Shield: " + time, Health_bar_GUI);
            }

        }

        if (playerBulletSpawner.machineGunBullets > 0) {
            GUI.Label(new Rect(10, 10, 100, 20), playerBulletSpawner.machineGunBullets.ToString());
        //GUI.Label(new Rect(new Vector2(100f,100f), new Vector2(100f,100f)), playerBulletSpawner.machineGunBullets).ToString();
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


	void PlayerDamage(int damage){
		if(playerHealth > 0){
			playerHealth -= damage;
		}
		if(playerHealth <= 0){
			playerHealth = 0;
			Debug.Log ("dead");
			Application.LoadLevel("GameOver");
			//Restart();
		}
		
	}

    void PlayerShield()
    {
        shield = true;
        countDown = 0.01f;
        print("shield start");
    }

    public void ScoreTracker(int n)
    {
        score += n;
    }

	void Update(){
        time -= countDown;
        if(time <= 0)
        {
            print("shield end");
            shield = false;
            countDown = 0.0f;
            time = 5;
        }
		//main.transform.position = new Vector3(player.transform.position.x +5, player.transform.position.y, player.transform.position.z -10);
		if (Input.GetKeyDown ("space")) {
			if (Time.time >= stunUseDelayTimeStamp) {
					stunEnemies = true;
					Debug.Log ("SPACE by!");
					stunDurationTimeStamp = Time.time + stunDuration;
					stunUseDelayTimeStamp = Time.time + stunUseDelay;
			}
			//else if (Time.time >= stunDurationTimeStamp) {
					//stunEnemies = false;
			//}
		}
		if (Time.time >= stunDurationTimeStamp) {
			stunEnemies = false;
		}
						
	}
	/*IEnumerator PlayerFreeze(float t){
		float s = movement.speed;
		movement.speed = 0;
		yield return new WaitForSeconds (t);
		movement.speed = s;

	}*/

    void Awake()
    {
        player = GameObject.Find("Player");
    }
	
}
