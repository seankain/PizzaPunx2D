using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{
    public Scene Level;
    public AudioSource selectionSound;
    public AudioSource music;
    private bool transitioning = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            transitioning = true;
            music.Stop();
            selectionSound.Play();

            //SceneManager.SetActiveScene(Level);
        }

        //Wait for selection sound to finish playing
        if(transitioning && !selectionSound.isPlaying) { 

            SceneManager.LoadScene(2);
        }
    }
}
