using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class platformSpawner : MonoBehaviour
{
    public GameObject Platforms;
    public GameObject platform;
    public BoxCollider2D spawnArea;
    public int platformNumber;
    public int minNumber;
    public float speed;




    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Camera.main.transform.position;
        Vector2 min = spawnArea.bounds.min;
        Vector2 max = spawnArea.bounds.max;
        if (platformNumber < minNumber)
        {
            Vector2 randomPosition;

            // Generate random position within the spawn area
           
                    randomPosition = new Vector2(Random.Range(min.x, max.x), spawnArea.bounds.center.y);
            // Spawn the platform at the random position
            GameObject newPlatform = Instantiate(platform, randomPosition, Quaternion.identity);
            newPlatform.transform.parent = Platforms.transform;
            platformNumber++;
        }



    }
}
               
            
            



