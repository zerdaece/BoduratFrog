using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Object
{
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    public float Speed = 5f;
    public GameObject objectobject;
    public List<GameObject> objectList;
    public GameObject objectContainer;
}
public class starSpawner : MonoBehaviour
{

    public Camera cam;
    [SerializeField] Object StarObject;
    [SerializeField] Object ObstacleObject;
    [SerializeField] Object ComboObject;
    [SerializeField] BoxCollider2D leftside;
    [SerializeField] BoxCollider2D upside;

    private Vector3 spawnerpos = new Vector3(0, 0, 0);//ehm bunu ben ekledim :p -goktug
                                                      // Start is called before the first frame update

    void Start()
    {
        spawnerpos.y = cam.transform.position.y;

        if (StarObject != null && StarObject.objectobject != null)
        {
            StartCoroutine(SpawnLoop(upside, StarObject));
        }

        if (ObstacleObject != null && ObstacleObject.objectobject != null)
        {
            StartCoroutine(SpawnLoop(upside, ObstacleObject));
        }
        if(ComboObject !=null && ComboObject.objectobject != null)
        {
            StartCoroutine(SpawnLoop(upside, ComboObject));
        }
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

    private IEnumerator SpawnLoop(BoxCollider2D spawnArea, Object objectToSpawn)
    {
        while (true) // This loop will run forever
        {
            // Wait for a random amount of time before spawning the next object
            float waitTime = Random.Range(objectToSpawn.minSpawnInterval, objectToSpawn.maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            // Calculate random position within the spawn area
            Vector2 randomPosition = GetRandomPositionInBounds(spawnArea.bounds);
            // Check if there's an inactive object in the pool
            GameObject newObject = objectToSpawn.objectList.Find(obj => !obj.activeInHierarchy);

            if (newObject == null)
            {
                // If no inactive object is available, instantiate a new one and add it to the pool
                newObject = Instantiate(objectToSpawn.objectobject, randomPosition, Quaternion.identity);
                newObject.transform.SetParent(objectToSpawn.objectContainer.transform);
                newObject.name = objectToSpawn.objectobject.name + objectToSpawn.objectList.Count;
                objectToSpawn.objectList.Add(newObject);
            }
            else
            {
                // Reuse the inactive object
                newObject.transform.position = randomPosition;
                newObject.transform.rotation = Quaternion.identity;
                newObject.SetActive(true);
            }

            // Get the Rigidbody2D component of the object
            Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();

            // Apply a force to the object depending on the spawn side
            if (spawnArea == leftside)
            {
                newObject.transform.Rotate(0, 180, 0);
                rb.AddForce(Vector2.right * objectToSpawn.Speed, ForceMode2D.Impulse);
            }
            else if (spawnArea == upside)
            {
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(Vector2.down * objectToSpawn.Speed, ForceMode2D.Impulse);
                print("eeh");
            }

            // Deactivate the object after a delay
            StartCoroutine(DisableAfterTime(newObject, 8f));
        }
    }

    private IEnumerator DisableAfterTime(GameObject objectToDisable, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
    }

    private Vector2 GetRandomPositionInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(randomX, randomY);
    }

}
