using UnityEngine;
using System.Collections;

public class HorsemanAgent : MonoBehaviour {
 private NavMeshAgent agent;
 public GameManager gameManager;
 private GameObject player;
 public float chaseSpeed = 2f;
 public State state;
 private bool alive;
 public float sightDist = 2;
 public GameObject bObject;
 public AudioClip shot;
 private Vector3 dis;
 public float defaultFireRate = .8f;
 private RaycastHit hit;
 private Vector3 shootingLocation;
 public GameObject sprite;
 private float nextBulletSpawnTimestamp;
 private float health;
 public enum State
 {
  IDLE,
  CHASE,
  ATTACK
 }


 // Use this for initialization
 void Start () {
  agent = GetComponent<NavMeshAgent>();
  player = GameObject.Find ("Player");
  agent.updatePosition = true;
  agent.updateRotation = false;
  alive = true;
  state = HorsemanAgent.State.IDLE;
  StartCoroutine ("FSM");
  health = 100;
 }

 // Update is called once per frame
 void Update () {
  if (GameManager.stunEnemies == true) {
   agent.Stop ();

  }
  else{
   agent.Resume ();
  }
  dis = transform.position - player.transform.position;
  Debug.DrawRay (transform.position, -dis, Color.green);
  if (Physics.Raycast (transform.position, -dis, out hit, sightDist)) {
   if (hit.collider.gameObject.tag == "Player") {
    state = HorsemanAgent.State.CHASE;
    if ((dis.z < 1.5 && dis.z > -1.5) && (dis.x < 1.5 && dis.x > -1.5)) {
     state = HorsemanAgent.State.ATTACK;

    }
   }
  } else {
   state = HorsemanAgent.State.IDLE;
  }
 }

 IEnumerator FSM(){
  while (alive) {
   switch (state) {
    case State.CHASE:
     Chase ();
     break;
    case State.IDLE:
     Idle ();
     break;
    case State.ATTACK:
     Attack ();
     break;
   }

   yield return null;
  }
 }

 void Idle() {
  agent.speed = 0;
 }

 void Chase() {
  agent.speed = chaseSpeed;
  agent.SetDestination (player.transform.position);
 }
 void Attack(){
  if (hit.collider.gameObject.tag == "wall") {
   state = HorsemanAgent.State.CHASE;
  }
  if (Time.time >= nextBulletSpawnTimestamp) {
   nextBulletSpawnTimestamp = Time.time + defaultFireRate;
   GameObject newBullet = Instantiate (bObject, sprite.transform.position, sprite.transform.rotation) as GameObject;
   AudioSource.PlayClipAtPoint (shot, transform.position);
   newBullet.tag = "bullets";
  }
 }

 void TakeDamage(int damage){
  if (health - damage >= 0) {
   health -= damage;

   sprite.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
  } else {
   alive = false;
   destory ();
  }
 }

 void destory()
 {
  gameManager.SendMessage("ScoreTracker", 10, SendMessageOptions.DontRequireReceiver);
  Destroy (sprite);
  Destroy(this.gameObject);

 }
}
