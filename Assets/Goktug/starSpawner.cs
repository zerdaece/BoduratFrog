using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class starSpawner : MonoBehaviour
{

    public Camera cam;
    [SerializeField] GameObject StarObject;
    [SerializeField] GameObject StarContainer;
    [SerializeField] GameObject ObstacleObject;
    [SerializeField] GameObject ObstacleContainer;
    [SerializeField] BoxCollider2D leftside;
    [SerializeField] BoxCollider2D rightside;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    [SerializeField] private float starSpeed = 5f; // Speed of the stars

    [SerializeField] List<GameObject> StarList;


    [SerializeField] List<GameObject> ObstacleList;
    private Vector3 spawnerpos = new Vector3(0, 0, 0);//ehm bunu ben ekledim :p -goktug
                                                      // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(SpawnStar(leftside));
        StartCoroutine(SpawnStar(rightside));
        spawnerpos.y = cam.transform.position.y;


    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerpos.y < (cam.transform.position.y + 1f))
        {
            spawnerpos.y += 2f;
        }
        Vector3 newPosition = new Vector3(transform.position.x, spawnerpos.y, transform.position.z);
        transform.position = newPosition;

    }
    private IEnumerator SpawnStar(BoxCollider2D spawnArea)
    {
        while (true)
        {

            // Wait for a random amount of time before spawning the next object
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

            // Calculate random position within the spawn area
            Vector2 randomPosition = GetRandomPositionInBounds(spawnArea.bounds);
            // Check if there's an inactive star in the pool
            GameObject newStar = StarList.Find(star => !star.activeInHierarchy);

            if (newStar == null)
            {
                // If no inactive star is available, instantiate a new one and add it to the pool
                newStar = Instantiate(StarObject, randomPosition, Quaternion.identity);
                newStar.transform.SetParent(StarContainer.transform);
                newStar.name = "Star_" + StarList.Count;
                StarList.Add(newStar);
            }
            else
            {
                // Reuse the inactive star
                newStar.transform.position = randomPosition;
                newStar.transform.rotation = Quaternion.identity;
                newStar.SetActive(true);
            }

            // Get the Rigidbody2D component of the star
            Rigidbody2D rb = newStar.GetComponent<Rigidbody2D>();

            // Apply a force to the star depending on the spawn side
            if (spawnArea == leftside)
            {

                newStar.transform.Rotate(0, 180, 0);
                rb.AddForce(Vector2.right * starSpeed, ForceMode2D.Impulse);

            }
            else if (spawnArea == rightside)
            {

                rb.AddForce(Vector2.left * starSpeed, ForceMode2D.Impulse);

            }
            yield return new WaitForSeconds(8f);
            // Destroy the newStar object after 3 seconds
            newStar.SetActive(false);

        }
    }
    private Vector2 GetRandomPositionInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(randomX, randomY);
    }

}
