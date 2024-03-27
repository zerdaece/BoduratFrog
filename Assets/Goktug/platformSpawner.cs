using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerpos.y < cam.transform.position.y)
        {
            spawnerpos.y +=  1.5f;
            transform.position = spawnerpos;
        }
        Vector2 min = spawnArea.bounds.min;
        Vector2 max = spawnArea.bounds.max;
        if (platformNumber < minNumber)
        {
            // Generate random position within the spawn area
            Vector2 randomPosition = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            // Spawn the platform at the random position
            GameObject newPlatform = Instantiate(platform, randomPosition, Quaternion.identity);
            newPlatform.transform.parent = Platforms.transform;
            platformNumber++;
        }
        


    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zemin"))
        {
            Destroy(other.gameObject);
            print(ScoreCounter.Score);
            ScoreCounter.Score++;
            platformNumber--;
        }

    }
}
