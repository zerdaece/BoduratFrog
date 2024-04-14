using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deadZone : MonoBehaviour
{
    private GameObject ScoreCounter;
    private ScoreCounter scoreCounter;
    public Camera cam;
    public GameObject player;
    public bool gameOver;
    public GameObject retry;
    public GameObject quit;
    [SerializeField] Vector3 deadzonepos = new Vector3(0, 0, 0);
    public static bool alive;
    private void Start()
    {
        ScoreCounter = GameObject.Find("ScoreCounter");
        scoreCounter = ScoreCounter.GetComponent<ScoreCounter>();
    }
    private void Update()
    {
        if (deadzonepos.y < cam.transform.position.y)
        {
            deadzonepos.y = cam.transform.position.y + 1.5f;
            transform.position = cam.transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //Oyunu durdurmak için
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            gameOver = true;
            Debug.Log("ÖLDÜN");
            retry.SetActive(true);
            quit.SetActive(true);
            alive = false;
            CoinSystem.UpdateCoinCount(scoreCounter.Score);
            print(CoinSystem.coin);
        }
    }
}//10 numaara kod yazmışın kral
