using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public float playerHealth = 100f;
    //public Player_Movement movement;
    public GUIStyle Health_bar_GUI;
    public Camera main;
	private bool shake = false;
	private Vector3 cameraPos;
	private float shakeAmount = 0.2f;
	private float decreaseFactor = 1.0f;
	private float shakeDuration = 0f;
    private GameObject player;
	public GameObject playerBulletSpawner;// used to gset number of bullets remaining
	private playerBulletSpawner spawner;
	public Text shieldText;
	public Image shieldImage;
	public Text machineGunBulletText;
	public Text shotGunBulletText;
	public Image machineGunImage;
	public Image ShotGunImage;
	public static bool stunEnemies;
	public Image health;
	public Image healthIcon;
	public Image stun;
    public Image view;
    public static int score = 0;
	public GameObject pauseMenu;
	private float stunDurationTimeStamp = 1;
	public static float stunDuration = 3f;
	public static float stunUseDelay = 15f;
	public float stunUseDelayTimeStamp;
	private bool paused = false;
    public static bool shield = false;
	public Text ScoreText;
    public float time = 5;
    public float countDown = 0.0f;
	private float stunCharger = 0;
	private int level;
    private static int diedLevel;

    void Start() {      
		spawner = playerBulletSpawner.GetComponent<playerBulletSpawner>();
    }

	void OnGUI() {
		if (playerHealth > 0 && playerHealth <= 100 && Application.loadedLevelName != "Winning"  && Application.loadedLevelName != "GameOver") {
			health.fillAmount = playerHealth / 100f;
            Color w = view.color;
            w.a = 1 - (playerHealth / 100) - 0.7f;
            view.color = w;
			ScoreText.text = "Score: " + score;
			/*Color n = healthIcon.color;
			n.g = 1 - (playerHealth / 100);
			n.b = 1 - (playerHealth / 100);
			healthIcon.color = n;*/
            
            // stun bar fill
            float elapsedSecs = Time.time - (stunUseDelayTimeStamp - stunUseDelay);
            float percent = (elapsedSecs/stunUseDelay);
            stun.fillAmount = percent;
			if (shield == true) {
				Color c = shieldImage.color;
				c.a = 1;
				shieldImage.color = c;
				shieldText.text = time.ToString ("F2");
			} else {
				Color c = shieldImage.color;
				c.a = 0;
				shieldImage.color = c;
				shieldText.text = "";

			}
			ScoreText.text = "Score: " + score;
			if (spawner.machineGunBullets > 0) {
				machineGunBulletText.text = spawner.machineGunBullets.ToString ();
				Color c = machineGunImage.color;
				c.a = 1;
				machineGunImage.color= c;
			} else {
				machineGunBulletText.text = "";
				Color c = machineGunImage.color;
				c.a = 0.4f;
				machineGunImage.color = c;
			}
			if (spawner.shotGunBullets > 0) {
				shotGunBulletText.text = spawner.shotGunBullets.ToString ();
				Color c = ShotGunImage.color;
				c.a = 1;
				ShotGunImage.color= c;
			} else {
				shotGunBulletText.text = "";
				Color c = ShotGunImage.color;
				c.a = 0.4f;
				ShotGunImage.color= c;
			}
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
			if (shield == false) {
				playerHealth -= damage;
				shake = true;
				shakeDuration = .2f;
			}

		}
		if(playerHealth <= 0){
            diedLevel = Application.loadedLevel;
			playerHealth = 100;
			Debug.Log ("dead");
			loadLose ();
			//Restart();
		}
		
	}
	public void loadNextScene(){
		level++;
		Application.LoadLevel (level);
	}
		
	void loadWin(){
		Application.LoadLevel ("6trump");
	}
	void loadLose(){
		Application.LoadLevel("GameOver");

	}

	public void loadQuit(){
		Application.Quit();
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
		cameraPos = main.transform.position;
//		stunCharger += 0.0003f;
        stunCharger = (stunUseDelayTimeStamp/Time.time);
        //Debug.Log(stunCharger);
		time -= countDown;
		if (time <= 0) {
			player.SendMessage ("Normal",  SendMessageOptions.DontRequireReceiver);
			shield = false;
			countDown = 0.0f;
			time = 5;
		}
		if(shield == true){
			//player.GetComponent<SpriteRenderer> ().color = new Color (Random.Range(70,220), Random.Range(70,220), Random.Range(70,220));
			player.SendMessage ("Blink", SendMessageOptions.DontRequireReceiver);
		}
		//main.transform.position = new Vector3(player.transform.position.x +5, player.transform.position.y, player.transform.position.z -10);
		//Debug.Log(stunUseDelayTimeStamp + "gM");
		if (Input.GetKeyDown (KeyCode.Space)) {
			
			if (Time.time >= stunUseDelayTimeStamp) {
					shake = true;
					shakeDuration = .8f;
					stunEnemies = true;
					stunCharger = 0;
					//Debug.Log ("SPACE by!");
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
		if (Input.GetKeyDown (KeyCode.Return) && Application.loadedLevelName == "GameOver") {
            Application.LoadLevel (diedLevel);
			score = 0;
		}
		if (shakeDuration > 0 && shake == true) {
			main.transform.position = cameraPos + Random.insideUnitSphere * shakeAmount;
			shakeDuration -= Time.deltaTime * decreaseFactor;
		} else {
			shakeDuration = 0f;
			main.transform.position = cameraPos;
			shake = false;
		}
	}

    void Awake()
    {
		level = Application.loadedLevel;
        player = GameObject.Find("Player");
    }
	
}
