using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

    public GameObject Player;
    public int Score = 0;
    public Text HighScoreText;
    public Text LastScoreText;
    public Text scoreText;
    public GameObject scoree_ui;
    
    
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        transform.position = Player.transform.position;
        HighScoreText.text= PlayerPrefs.GetInt("HighScore", 0).ToString("HighScore: "+Score.ToString());
        LastScoreText.text= PlayerPrefs.GetInt("LastScore", 0).ToString("LastScore: "+Score.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zemin"))
            if (other.GetComponent<platform>().counted == false)
            {  
                scoree_ui.SetActive(true);
                
                Score++;
                PlayerPrefs.SetInt("LastScore", Score);
                LastScoreText.text= Score.ToString("LastScore: "+Score.ToString());
                scoreText.text =  Score.ToString();
               
                if(Score > PlayerPrefs.GetInt("HighScore", 0))
                {
                    PlayerPrefs.SetInt("HighScore", Score);
                    HighScoreText.text = Score.ToString("HighScore: "+Score.ToString());
                }
                other.GetComponent<platform>().counted = true;
               
            }
    }
    }
