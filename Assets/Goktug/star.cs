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
            
          
            Destroy(gameObject);
        }

    }
    
}
