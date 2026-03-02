using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class backgrounhandle : MonoBehaviour
{
    
    public Transform[] backgrounds; // Üç farklı background prefabı (farklı görseller için)
    public int backgroundsPerRow = 3; // Her satırda kaç background olacak (yatayda 3)
    private List<Transform> activeBackgrounds = new List<Transform>(); // Aktif backgroundlar

    private Camera mainCamera;
    private float lastCameraY; // Kameranın son y pozisyonunu saklar

    void Start()
    {
        mainCamera = Camera.main;
        lastCameraY = mainCamera.transform.position.y;

        // İlk satırı oluştur (yatayda 3 background)
        float bgWidth = GetBackgroundWidth();
        float bgHeight = GetBackgroundHeight();
        Vector3 startPos = backgrounds[0].position;
        for (int i = 0; i < backgroundsPerRow; i++)
        {
            Transform bg = Instantiate(backgrounds[0], new Vector3(startPos.x + i * bgWidth, startPos.y, startPos.z), Quaternion.identity, backgrounds[0].parent);
            bg.localScale = backgrounds[0].localScale;
            activeBackgrounds.Add(bg);
        }
    }

    void Update()
    {
        // Kameranın yüksekliğini kontrol et
        float cameraY = mainCamera.transform.position.y;


        // Kamera yukarı doğru hareket ettiyse
        if (cameraY > lastCameraY)
        {
            float bgHeight = GetBackgroundHeight();
            float bgWidth = GetBackgroundWidth();

            // En üstteki satırın y pozisyonunu bul
            float maxY = float.MinValue;
            foreach (Transform bg in activeBackgrounds)
            {
                if (bg.position.y > maxY)
                    maxY = bg.position.y;
            }

            // Kamera, en üstteki satırın yarısını geçtiyse yeni bir satır spawnla
            if (mainCamera.transform.position.y > maxY - bgHeight / 2f)
            {
                // Yeni satırın y pozisyonu
                float newY = maxY + bgHeight;
                // Yatayda 3 background oluştur
                for (int i = 0; i < backgroundsPerRow; i++)
                {
                    Transform bg = Instantiate(backgrounds[0], new Vector3(activeBackgrounds[0].position.x + i * bgWidth, newY, activeBackgrounds[0].position.z), Quaternion.identity, backgrounds[0].parent);
                    bg.localScale = backgrounds[0].localScale;
                    activeBackgrounds.Add(bg);
                }
            }
        }

        lastCameraY = cameraY; // Son kamera y pozisyonunu güncelle
    }

    // Yeniden konumlandırma için background yüksekliğini al
    float GetBackgroundHeight()
    {
        SpriteRenderer spriteRenderer = backgrounds[0].GetComponent<SpriteRenderer>();
        return spriteRenderer.bounds.size.y;
    }

    // Background genişliğini al
    float GetBackgroundWidth()
    {
        SpriteRenderer spriteRenderer = backgrounds[0].GetComponent<SpriteRenderer>();
        return spriteRenderer.bounds.size.x;
    }

}
