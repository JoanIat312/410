using UnityEngine;
using System.Collections;

public class enemyFlames : MonoBehaviour
{

	// Use this for initialization
    public AudioClip explosion;
    public float moveSpeed = 0;
    private Vector3 objectPos;
    private Vector3 targetPos;
	private Vector3 dis;
	private Quaternion num;
	private float angle;
	private Rigidbody rb;
	private bool hitWall;
	public int defaultDamage = 20;
    Animator anim;
	public GameManager gameManager;
    public float flameLifespan = 2f;

	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
        anim = GetComponent<Animator>();
		hitWall = false;
		//defaultDamage = 20;
        objectPos = transform.position;
        targetPos = GameObject.Find("Player").transform.position;

//        Vector3 zConvertedObjectPos = new Vector3(objectPos.x, 1, objectPos.y);
//        Vector3 zConvertedTargetPos = new Vector3(targetPos.x, 1, targetPos.y);
//
//        dis = zConvertedTargetPos - zConvertedObjectPos;
        dis = targetPos - objectPos;
        dis = dis + new Vector3 (Random.Range (-1, 1), 0, Random.Range (-1, 1));
        dis.Normalize();
        if (gameObject.name != "enemyFlames")
        {      
          Destroy(gameObject, flameLifespan); // flames should dissapate
        }
    }

	// Update is called once per frame
	void Update ()
	{
        
    }

	void OnTriggerEnter (Collider col)

    {               
  
        if (col.gameObject.tag == "wall" && gameObject.name != "enemyFlames")
        {
			hitWall = true;
			anim.Play("bulletExplosion", 0, 0);
			AudioSource.PlayClipAtPoint(explosion, transform.position);
            Destroy(gameObject, .4f);
        }
        if (col.gameObject.tag == "Player" && gameObject.name != "enemyFlames")
        {
			col.gameObject.SendMessage("Damaged", SendMessageOptions.DontRequireReceiver);
//            Debug.Log("defaultDamage:" + defaultDamage);
			gameManager.SendMessage("PlayerDamage", defaultDamage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
 
    void FixedUpdate ()
	{
        // random flame sizes
        float scale = Random.Range(.5f, 2f);
        transform.localScale = new Vector3 (scale, scale, 1);

        if (gameObject.name != "enemyFlames")
        {
            if (hitWall == true) {
				rb.velocity = new Vector3 (0, 0, 0);	
			} else {
                rb.velocity = (dis * moveSpeed);
			}
        }
	}
}
