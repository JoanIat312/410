using UnityEngine;
using System.Collections;
[RequireComponent (typeof(AudioSource))]
public class MoviePlay : MonoBehaviour {
    
 private MovieTexture movie;

    // Use this for initialization
    void Start() {
//        GetComponent<Renderer>().material.mainTexture = movTexture;
//        movTexture.Play();

            Renderer r = GetComponent<Renderer>();
            AudioSource audio = GetComponent<AudioSource>();
            movie = (MovieTexture)r.material.mainTexture;
            audio.clip = movie.audioClip;
//            if (movie.isPlaying) {
//                movie.Pause();
//            }
//            else {
                movie.Play();
                audio.Play();

//            }
    }
	
	// Update is called once per frame
	void Update () {
      if (Input.GetMouseButton(0))
      {
       if (Application.loadedLevelName == "ending-movie")
       {
        Application.LoadLevel("Title");
       }
       else
       {
        Application.LoadLevel(Application.loadedLevel + 1);
       }
      }
      else if (!movie.isPlaying)
      {
       Debug.Log("movie end");
       if (Application.loadedLevelName == "ending-movie")
       {
        Application.LoadLevel("Title");
       }
       else
       {
        Application.LoadLevel(Application.loadedLevel + 1);
       }
      }
     }
}