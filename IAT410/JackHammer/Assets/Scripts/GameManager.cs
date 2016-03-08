using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public float playerHealth = 100f;
    //public Player_Movement movement;
    public GUIStyle Health_bar_GUI;
    public Camera main;
    private GameObject player;
	public GameObject playerBulletSpawner;// used to gset number of bullets remaining
	private playerBulletSpawner spawner;
	public Text shieldText;
	public Text machineGunBulletText;
	public Text shotGunBulletText;
	public static bool stunEnemies;
	public Image health;
	public Image stun;
    public static int score = 0;
	public GameObject pauseMenu;
	private float stunDurationTimeStamp;
	public static float stunDuration = 3f;
	public static float stunUseDelay = 10f;
	private float stunUseDelayTimeStamp;
	private bool paused = false;
    public static bool shield = false;
	public Text ScoreText;
    public float time = 5;
    public float countDown = 0.0f;
	private float stunCharger = 0;
	private int level = 0;
    void Start() {
       
		spawner = playerBulletSpawner.GetComponent<playerBulletSpawner>();
    }

	void OnGUI() {

		if (playerHealth > 0 && playerHealth <= 100 && Application.loadedLevel != 3) {
			health.fillAmount = playerHealth / 100f ;
			stun.fillAmount = stunCharger;
			if (shield == true) {
				shieldText.text = time.ToString ("F2");
			} else {
				shieldText.text = "";
			}
			ScoreText.text = "Score: " + score;
			machineGunBulletText.text = spawner.machineGunBullets.ToString ();
			shotGunBulletText.text = spawner.shotGunBullets.ToString ();
		} else if (Application.loadedLevel == 3) {
			ScoreText.text = "Score: " + score;
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


	void PlayerDamage(float damage){
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
	void loadNextScene(){
		level++;
		Application.LoadLevel (level);
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
		stunCharger += 0.003f;
		time -= countDown;
		if (time <= 0) {
			shield = false;
			print ("shield end");
			countDown = 0.0f;
			time = 5;
		}
		//main.transform.position = new Vector3(player.transform.position.x +5, player.transform.position.y, player.transform.position.z -10);
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (Time.time >= stunUseDelayTimeStamp) {
					stunEnemies = true;
					stunCharger = 0;
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
		if (Input.GetKeyDown (KeyCode.Escape) && paused == false) {
			pauseMenu.active = true;
			paused = true;
			Time.timeScale = 0.0f;
		} else if (Input.GetKeyDown (KeyCode.Escape) && paused == true) {
			pauseMenu.active = false;
			paused = false;
			Time.timeScale = 1;
		}

	}

    void Awake()
    {
        player = GameObject.Find("Player");
    }
	
}
