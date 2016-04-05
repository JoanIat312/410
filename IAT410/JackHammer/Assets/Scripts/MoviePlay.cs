using UnityEngine;
using System.Collections;
[RequireComponent (typeof(AudioSource))]

public class MoviePlay : MonoBehaviour {
    
 private MovieTexture movie;
 private bool canClick = false;

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
                canClick = false;
                StartCoroutine("clickDelay");
//            }
    }
	
	// Update is called once per frame
	void Update () {
          if (Input.GetMouseButton(0) && canClick == true)
          {
                Debug.Log("CLICKED U SUCKA!");
                // can't skip last movie
               if (Application.loadedLevelName != "ending-movie")
               {
                    Application.LoadLevel(Application.loadedLevel + 1);
               }
          }
          else if (!movie.isPlaying) // next scene if movie ends
          {
               Debug.Log("movie end");
               if (Application.loadedLevelName == "ending-movie")
               {
                    Application.LoadLevel("Title"); // back to title if you beat the game
               }
               else
               {
                    Application.LoadLevel(Application.loadedLevel + 1);
               }
          }
     }
     IEnumerator clickDelay() {
          yield return new WaitForSeconds (2.5f); 
          print("Done " + Time.time);
          canClick = true;
     }
}