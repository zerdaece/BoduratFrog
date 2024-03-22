using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player ;
    private Vector3 offset = new Vector3(0,2,-2);
    private Vector3 campos = new Vector3(0, 0, 0);//ehm bunu ben ekledim :p -goktug

    // Start is called before the first frame update
    void Start()
    {
        campos.y = player.transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (campos.y < (player.transform.position.y - 2f))
        {
            campos.y = player.transform.position.y - 2f;
        }
        Vector3 newPosition = new Vector3(transform.position.x, campos.y + offset.y, transform.position.z);
        transform.position = newPosition;
        
    }
}
