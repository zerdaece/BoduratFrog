using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboObject : MonoBehaviour
{
    [SerializeField] int comboValue = 1; 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<JumpMovement>().Combocounter += comboValue; // Reduce speed by half
            gameObject.SetActive(false);
        }

    }
    
}
