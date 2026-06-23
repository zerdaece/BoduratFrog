
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject playButton;
  public GameObject PauseButton;
  public GameObject ShopButton;
  public GameObject score;
  public GameObject lastScore;
  public GameObject highScore;
  
  public Animator fingeranimator;
  public GameObject finger;
  public GameObject Title;
  
  public static bool isGameStarted = false;





  private void Awake()
  {
    isGameStarted = false;
    finger.SetActive(false);
    PauseButton.SetActive(false);
    ShopButton.SetActive(true);
    score.SetActive(true);
    highScore.SetActive(true);
    lastScore.SetActive(true);
    Title.SetActive(true);
    QualitySettings.vSyncCount = 0;
    Application.targetFrameRate = 144;
  }



  // Update is called once per frame
  public void Play()
  {
    isGameStarted = true;
    finger.SetActive(true);
    Invoke("FingerAnimFinish", 3f);

    Title.SetActive(false);
    playButton.SetActive(false);
    score.SetActive(true);
    ShopButton.SetActive(false);
    highScore.SetActive(false);
    lastScore.SetActive(false);
   
  }
  public void Retry()
  {
    isGameStarted = false;
    SceneManager.LoadScene(0);
  }
  private void FingerAnim()
  {

    fingeranimator.enabled = true;
  }
  private void FingerAnimFinish()
  {
    fingeranimator.enabled = false;
    Destroy(finger);
  }


}
