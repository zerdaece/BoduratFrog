using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadZone : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public bool gameOver;
    private void Update()
    {
        transform.position = cam.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            gameOver = true;
        }
    }
}
