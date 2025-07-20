using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class platformSpawner : MonoBehaviour
{
    public GameObject Platforms;
    public GameObject platform;
    public GameObject movablePlatform;
    public BoxCollider2D spawnArea;
    public int platformNumber;
    public int minNumber;
    private GameObject lastPlatform;

    [Header("Pool Settings")]
    public int poolSize = 20;
    private Queue<GameObject> platformPool = new Queue<GameObject>();




    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.transform.position;
        InitializePool();
    }

    void InitializePool()
    {
        // Create initial pool of platforms
        for (int i = 0; i < poolSize; i++)
        {
            GameObject pooledPlatform = Instantiate(platform, Vector3.zero, Quaternion.identity);
            pooledPlatform.transform.parent = Platforms.transform;
            pooledPlatform.SetActive(false);
            platformPool.Enqueue(pooledPlatform);
        }
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
            int randomNumber = Random.Range(1, 7);

            // Spawn the platform at the random position
            GameObject newPlatform = GetPlatformFromPool(randomPosition);
            if (newPlatform != null)
            {
                platformNumber++;
                if (randomNumber < 2)
                {
                    MovablePlatform movableComponent = newPlatform.GetComponentInChildren<MovablePlatform>();
                    if (movableComponent != null)
                    {
                        movableComponent.enabled = true;
                    }
                }
                if (lastPlatform != null)
                {
                    float platformHeight = newPlatform.GetComponentInChildren<SpriteRenderer>().bounds.size.y; // Yeni platformun yüksekliği
                    float lastPlatformY = lastPlatform.transform.position.y; // Önceki platformun y eksenindeki pozisyonu
                    newPlatform.transform.position = new Vector2(newPlatform.transform.position.x, lastPlatformY + platformHeight + 1.2f);
                }

                // Yeni platformu önceki platform olarak ata
                lastPlatform = newPlatform;
            }
        }



    }

    GameObject GetPlatformFromPool(Vector2 position)
    {
        GameObject pooledPlatform;
        
        if (platformPool.Count > 0)
        {
            // Pool'dan platform al
            pooledPlatform = platformPool.Dequeue();
        }
        else
        {
            // Pool boşsa yeni platform oluştur
            pooledPlatform = Instantiate(platform, Vector3.zero, Quaternion.identity);
            pooledPlatform.transform.parent = Platforms.transform;
        }
        
        // Platform'u sıfırla ve konumlandır
        ResetPlatform(pooledPlatform);
        pooledPlatform.transform.position = position;
        pooledPlatform.SetActive(true);
        
        return pooledPlatform;
    }

    void ResetPlatform(GameObject platformToReset)
    {
        // Platform'u varsayılan duruma getir
        MovablePlatform movableComponent = platformToReset.GetComponentInChildren<MovablePlatform>();
        if (movableComponent != null)
        {
            movableComponent.enabled = false;
        }
        
        // Platform script'ini sıfırla
        platform platformScript = platformToReset.GetComponent<platform>();
        if (platformScript != null)
        {
            platformScript.stepped = false;
            platformScript.counted = false;
        }
        
        platformToReset.transform.rotation = Quaternion.identity;
    }

    public void ReturnPlatformToPool(GameObject platformToReturn)
    {
        // Platform'u sıfırla ve pool'a geri koy
        ResetPlatform(platformToReturn);
        platformToReturn.SetActive(false);
        platformPool.Enqueue(platformToReturn);
    }
}