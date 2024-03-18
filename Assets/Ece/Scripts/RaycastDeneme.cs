using UnityEngine;

public class RaycastDeneme : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 1f; // Raycast mesafesi
    [SerializeField] private LayerMask layerMask; // Sadece belirli layerlara çarpan nesneleri kontrol etmek için
    private BoxCollider2D boxCollider2D;

    void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        // Nesnenin box collider 2D'sini al
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            // Box collider'ın alt orta noktasını bul
            Vector2 origin = (Vector2)transform.position + boxCollider.offset - new Vector2(0f, boxCollider.size.y / 2);

            // Y ekseninde raycast'i aşağıya doğru at
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, raycastDistance, layerMask);

            // Eğer raycast bir şeye çarparsa
            if (hit.collider != null)
            {
                // Çarptığı nesnenin tag'ini kontrol et
                if (hit.collider.CompareTag("Zemin"))
                {
                    // Eğer tag "zemin" ise debug mesajı göster
                    Debug.Log("Zemine değdiniz!");
                    hit.collider.isTrigger = false;
                }
            }

            // Debug için raycast'i göster
            Debug.DrawRay(origin, Vector2.down * raycastDistance, Color.red);
        }
        else
        {
            // Eğer nesne bir box collider 2D içermiyorsa uyarı ver
            Debug.LogWarning("Bu nesne bir BoxCollider2D içermiyor!");
        }
    }
}

