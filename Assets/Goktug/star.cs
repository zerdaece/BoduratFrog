using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class star : MonoBehaviour
{
    private GameObject ScoreCounter;
    private ScoreCounter scoreCounter;
   
    //public Text addfivescore;
    // Start is called before the first frame update
    void Start()
    {
        ScoreCounter = GameObject.Find("ScoreCounter");
        scoreCounter = ScoreCounter.GetComponent<ScoreCounter>();
        
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scoreCounter.Score += 5;
            
            scoreCounter.scoreText.text = scoreCounter.Score.ToString();
            
            PlayerPrefs.SetInt("LastScore", scoreCounter.Score);
            scoreCounter.LastScoreText.text= "LastScore: " + scoreCounter.Score.ToString();

            if(scoreCounter.Score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", scoreCounter.Score);
                scoreCounter.HighScoreText.text = "HighScore: " + scoreCounter.Score.ToString();
            }

            gameObject.SetActive(false);
        }

    }
    
}
