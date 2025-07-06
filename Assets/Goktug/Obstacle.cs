using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{

    void Start()
    {

        
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<JumpMovement>().combocount -= 1; // Reduce speed by half
            gameObject.SetActive(false);
        }

    }
    
}
