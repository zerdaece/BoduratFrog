using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class deadZone : MonoBehaviour
{
    private GameObject ScoreCounter;
    private ScoreCounter scoreCounter;
    public Camera cam;
    public GameObject player;
    public bool gameOver;
    public GameManager gameManager;
    public AudioSource deadSound;
    [SerializeField] Vector3 deadzonepos = new Vector3(0, 0, 0);

    public static bool alive;
    private void Start()
    {
        ScoreCounter = GameObject.Find("ScoreCounter");
        scoreCounter = ScoreCounter.GetComponent<ScoreCounter>();
        deadSound = GetComponent<AudioSource>();
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
            deadSound.Play();
            gameOver = true;
            Debug.Log("ÖLDÜN");
            alive = false;
            Invoke("CallRetry", 1f);
            CoinSystem.UpdateCoinCount(scoreCounter.Score);
            print(CoinSystem.coin);
        }
    }

    public void CallRetry()
    {
        gameManager.Retry();
    }

}//10 numaara kod yazmışın kral
