using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class platformSpawner : MonoBehaviour
{
    public GameObject Platforms;
    public GameObject platform;
    public BoxCollider2D spawnArea;
    [SerializeField] Vector3 spawnerpos = new Vector3(0, 0, 0);
    public BoxCollider2D destroyArea;
    public Camera cam;
    public int platformNumber;
    public int minNumber;
    public float speed;
    private Vector2 lastPlatformPosition;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, cam.transform.position, speed);
        Vector2 min = spawnArea.bounds.min;
        Vector2 max = spawnArea.bounds.max;
        if (platformNumber < minNumber)
        {
            Vector2 randomPosition;
            float distanceThreshold = 0.2f; // adjust this value to change the minimum distance between platforms


            // Generate random position within the spawn area
            do
            {
                randomPosition = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            }
            while ((randomPosition.y - lastPlatformPosition.y) < distanceThreshold);


            // Spawn the platform at the random position
            GameObject newPlatform = Instantiate(platform, randomPosition, Quaternion.identity);
            newPlatform.transform.parent = Platforms.transform;
            platformNumber++;
            // update the last platform position
            lastPlatformPosition = randomPosition;
        }
        


    }
}
