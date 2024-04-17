using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject playButton;
   public Camera cam;
    private void Awake()
    {
        Pause();
        cam.transform.position = new Vector3(0, 0,-1);
        
    }
    public void Pause()
    {
        Time.timeScale = 0;

    }
    
    // Update is called once per frame
    public void Play()
    {
      Time.timeScale = 1;
      playButton.SetActive(false);
    }
    public void Retry()
    {
      SceneManager.LoadScene(0);
      Time.timeScale= 1;
      
    }
    public void Quit()
    {
        Application.Quit();

    }
   
}
