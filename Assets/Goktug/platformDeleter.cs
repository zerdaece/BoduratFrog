using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformDeleter : MonoBehaviour
{
    public Camera cam;
    platformSpawner platformSpawner;
    public GameObject PlatformSpawner;

    // Start is called before the first frame update
    void Start()
    {
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
            Destroy(other.gameObject);
            print(ScoreCounter.Score);
            ScoreCounter.Score++;
            platformSpawner.platformNumber--;
        }

    }
}
