using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformSpawner : MonoBehaviour
{
    public GameObject platform;
    public BoxCollider2D spawnArea;
    public BoxCollider2D destroyArea;
    public Camera cam;
    public int platformNumber;
    public int minNumber;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.transform.position;
        Vector2 min = spawnArea.bounds.min;
        Vector2 max = spawnArea.bounds.max;
        if (platformNumber < minNumber)
        {
            // Generate random position within the spawn area
            Vector2 randomPosition = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            // Spawn the platform at the random position
            GameObject newPlatform = Instantiate(platform, randomPosition, Quaternion.identity);
            platformNumber++;
            print("spawned");
        }
        


    }
     void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Zemin"))
        {
            print("destroyed");
            Destroy(platform);
            platformNumber--;
        }

    }
}
