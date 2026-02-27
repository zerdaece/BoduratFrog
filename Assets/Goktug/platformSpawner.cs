using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class platformSpawner : MonoBehaviour
    
{
    [Header("Platform Prefabs")]
    public GameObject platformPrefab1;
    public GameObject platformPrefab2;
    public GameObject platformPrefab3;
    public GameObject Platforms;
    public GameObject platform;
    public GameObject movablePlatform;
    public BoxCollider2D spawnArea;
    public int platformNumber;
    public int minNumber;
    private GameObject lastPlatform;

    [Header("Player Reference")]
    public Transform player; // Oyuncunun Transform'u

    [Header("Pool Settings")]
    public int poolSize = 20;
    private Queue<GameObject> platformPool1 = new Queue<GameObject>();
    private Queue<GameObject> platformPool2 = new Queue<GameObject>();
    private Queue<GameObject> platformPool3 = new Queue<GameObject>();




    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.transform.position;
        InitializePool();
    }

    void InitializePool()
    {
        // Create initial pool for each platform type
        for (int i = 0; i < poolSize; i++)
        {
            GameObject pooledPlatform1 = Instantiate(platformPrefab1, Vector3.zero, Quaternion.identity);
            pooledPlatform1.transform.parent = Platforms.transform;
            pooledPlatform1.SetActive(false);
            platformPool1.Enqueue(pooledPlatform1);

            GameObject pooledPlatform2 = Instantiate(platformPrefab2, Vector3.zero, Quaternion.identity);
            pooledPlatform2.transform.parent = Platforms.transform;
            pooledPlatform2.SetActive(false);
            platformPool2.Enqueue(pooledPlatform2);

            GameObject pooledPlatform3 = Instantiate(platformPrefab3, Vector3.zero, Quaternion.identity);
            pooledPlatform3.transform.parent = Platforms.transform;
            pooledPlatform3.SetActive(false);
            platformPool3.Enqueue(pooledPlatform3);
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
        GameObject pooledPlatform = null;
        Queue<GameObject> selectedPool = null;
        GameObject selectedPrefab = null;
        float playerY = player != null ? player.position.y : 0f;

        if (playerY < 21f)
        {
            if (playerY < 16f)
            {
                selectedPool = platformPool1;
                selectedPrefab = platformPrefab1;
            }
            else
            {
                selectedPool = platformPool2;
                selectedPrefab = platformPrefab2;
            }
        }
        else if (playerY < 51f)
        {
            selectedPool = platformPool2;
            selectedPrefab = platformPrefab2;
        }
        else
        {
            selectedPool = platformPool3;
            selectedPrefab = platformPrefab3;
        }

        if (selectedPool != null && selectedPool.Count > 0)
        {
            pooledPlatform = selectedPool.Dequeue();
        }
        else
        {
            pooledPlatform = Instantiate(selectedPrefab, Vector3.zero, Quaternion.identity);
            pooledPlatform.transform.parent = Platforms.transform;
        }

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
        // Platform'u sıfırla ve doğru pool'a geri koy
        ResetPlatform(platformToReturn);
        platformToReturn.SetActive(false);
        if (platformToReturn.name.Contains(platformPrefab1.name))
        {
            platformPool1.Enqueue(platformToReturn);
        }
        else if (platformToReturn.name.Contains(platformPrefab2.name))
        {
            platformPool2.Enqueue(platformToReturn);
        }
        else if (platformToReturn.name.Contains(platformPrefab3.name))
        {
            platformPool3.Enqueue(platformToReturn);
        }
        else
        {
            // Default olarak ilk pool'a ekle
            platformPool1.Enqueue(platformToReturn);
        }
    }
}