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
  public GameObject soundButton;
  public GameObject etiketButton;
  public GameObject score;
  public GameObject lastScore;
  public GameObject highScore;
  public GameObject Logobutton;
  public Animator fingeranimator;
  public GameObject finger;
  
  


  private void Awake()
  {
    finger.SetActive(false);
    Time.timeScale = 0;
    soundButton.SetActive(true);
    etiketButton.SetActive(true);
    score.SetActive(true);
    highScore.SetActive(true);
    lastScore.SetActive(true);
  }



  // Update is called once per frame
  public void Play()
  {
    Time.timeScale = 1;
    finger.SetActive(true);
    Invoke("FingerAnimFinish", 3f);
  
    
    playButton.SetActive(false);
    score.SetActive(true);
    soundButton.SetActive(true);
    etiketButton.SetActive(true);
    highScore.SetActive(false);
    lastScore.SetActive(false);
    Logobutton.SetActive(false);
    
  }
  public void Retry()
  {
    Time.timeScale = 1;
    SceneManager.LoadScene(0);
  }
   private void FingerAnim()
   {
    
    fingeranimator.enabled = true;
   }
   private void FingerAnimFinish()
   {
    fingeranimator.enabled= false;
    Destroy(finger);
   }


}
