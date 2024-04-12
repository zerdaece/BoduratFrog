using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformDeleter : MonoBehaviour
{
    public Camera cam;
    private GameObject ScoreCounter;
    private ScoreCounter scoreCounter;
    platformSpawner platformSpawner;
    public GameObject PlatformSpawner;

    // Start is called before the first frame update
    void Start()
    {
        ScoreCounter = GameObject.Find("ScoreCounter");
        scoreCounter = ScoreCounter.GetComponent<ScoreCounter>();
        platformSpawner = PlatformSpawner.GetComponent<platformSpawner>();
        transform.position = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,cam.transform.position,platformSpawner.speed);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zemin"))
        {
            if (other.GetComponent<platform>().counted == false)
                scoreCounter.Score++;
            print(scoreCounter.Score);
            Destroy(other.gameObject);
            platformSpawner.platformNumber--;
        }

    }
}
