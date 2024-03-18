using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadZone : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public bool gameOver;
    [SerializeField] Vector3 deadzonepos = new Vector3(0, 0, 0); 

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
        }
    }
}//10 numaara kod yazmışın kral
