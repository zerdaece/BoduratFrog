using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class platform : MonoBehaviour
{
    public bool counted;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            counted = true;
    }
}
