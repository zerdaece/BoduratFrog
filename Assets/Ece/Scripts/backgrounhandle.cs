using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class backgrounhandle : MonoBehaviour
{
    
    public Transform[] backgrounds; // Üç farklı background nesnesi için dizi

    private Camera mainCamera;
    private float lastCameraY; // Kameranın son y pozisyonunu saklar

    void Start()
    {
        mainCamera = Camera.main;
        lastCameraY = mainCamera.transform.position.y;
    }

    void Update()
    {
        // Kameranın yüksekliğini kontrol et
        float cameraY = mainCamera.transform.position.y;

        // Kamera yukarı doğru hareket ettiyse
        if (cameraY > lastCameraY)
        {
            // Her background nesnesi için kontrol et
            foreach (Transform background in backgrounds)
            {
                // Eğer background nesnesinin yüksekliği kameranın yüksekliğinin altındaysa
                if (background.position.y + GetBackgroundHeight() < mainCamera.transform.position.y)
                {
                    // Yeniden konumlandırma işlemi
                    float yOffset = GetBackgroundHeight();
                    background.position = new Vector3(background.position.x, background.position.y + yOffset * 2, background.position.z);
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

}
