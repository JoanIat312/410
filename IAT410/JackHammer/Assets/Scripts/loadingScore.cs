using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class loadingScore : MonoBehaviour {

    Text txt;
    private int score;
	// Use this for initialization
	void Start () {
        txt = gameObject.GetComponent<Text>();
        score = GameManager.score;
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = "Score: " + score;
    }

}
