using UnityEngine;
using System.Collections;

public class MoviePlay : MonoBehaviour {
    
    public MovieTexture movTexture;

    // Use this for initialization
    void Start() {
//        GetComponent<Renderer>().material.mainTexture = movTexture;
//        movTexture.Play();

            Renderer r = GetComponent<Renderer>();
            MovieTexture movie = (MovieTexture)r.material.mainTexture;

//            if (movie.isPlaying) {
//                movie.Pause();
//            }
//            else {
                movie.Play();
//            }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}