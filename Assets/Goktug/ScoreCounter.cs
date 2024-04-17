using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

    public GameObject Player;
    public int Score = 0;
    public Text scoreText;
    public GameObject scoree_ui;
    
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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
                scoree_ui.SetActive(true);
                scoreText.text =  Score.ToString();
                Score++;
                
                other.GetComponent<platform>().counted = true;
               
            }
    }
    }
