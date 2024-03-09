using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player ;
    private Vector3 offset = new Vector3(0,2,-2);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, player.transform.position.y + offset.y, transform.position.z);
        transform.position = newPosition;
        
    }
}
