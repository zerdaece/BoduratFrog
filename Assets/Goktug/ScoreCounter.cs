using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

    public GameObject Player;
    public int Score = -1;
    public Text scoreText;
    
    void Awake()
    {
        transform.position = Player.transform.position;
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
                Score++;
                scoreText.text = "Score: " + Score.ToString();
                other.GetComponent<platform>().counted = true;
               
            }
    }
    }
