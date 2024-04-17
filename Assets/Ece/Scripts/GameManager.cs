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
  //public Camera cam;
  public GameObject score;
  public GameObject lastScore;
  public GameObject backgroundAnim;


  private void Awake()
  {
    Time.timeScale = 0;

    score.SetActive(true);
    // cam.transform.position = new Vector3(0, 3,-1);

  }

  public void Pause()
  {
    Time.timeScale = 0;
    score.SetActive(true);
    lastScore.SetActive(true);
  }

  // Update is called once per frame
  public void Play()
  {
    Time.timeScale = 1;
    playButton.SetActive(false);
    score.SetActive(true);
    lastScore.SetActive(false);
  }
  public void Retry()
  {
    Time.timeScale = 1;
    backgroundAnim.SetActive(true);
    Invoke(nameof(PlayAnim), 1.4f);
    score.SetActive(true);
  }
  private void PlayAnim()
  {


    SceneManager.LoadScene(0);
    lastScore.SetActive(true);
  }

  public void Quit()
  {
    Application.Quit();

  }

}
