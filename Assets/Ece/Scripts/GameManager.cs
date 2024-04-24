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
    lastScore.SetActive(true);
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
    lastScore.SetActive(false);
    Logo.SetActive(false);
  }
  public void Retry()
  {
    Time.timeScale = 1;
    SceneManager.LoadScene(0);
  }



}
