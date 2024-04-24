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
  //public Camera cam;
  public GameObject score;
  public GameObject lastScore;
  public GameObject backgroundAnim;
  public GameObject highScore;

public GameObject Logo;

  private void Awake()
  {
    Time.timeScale = 0;
    soundButton.SetActive(true);
    etiketButton.SetActive(true);
    score.SetActive(true);
    highScore.SetActive(true);
    
  }



  // Update is called once per frame
  public void Play()
  {
    Time.timeScale = 1;
    playButton.SetActive(false);
    score.SetActive(true);
    soundButton.SetActive(true);
    etiketButton.SetActive(true);
    highScore.SetActive(false);
    Logo.SetActive(false);
  }
  public void Retry()
  {
    Time.timeScale = 1;
    //backgroundAnim.SetActive(true);
    //Invoke(nameof(PlayAnim), 1.4f);
    SceneManager.LoadScene(0);
    //Logo.SetActive(false);
    //score.SetActive(true);
  }
  /*private void PlayAnim()
  {

    SceneManager.LoadScene(0);
    
  }*/

  /*public void Quit()
  {
    Application.Quit();

  }*/

}
