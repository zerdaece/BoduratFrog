using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player ;
    private Vector3 offset = new Vector3(0,3,-2);
    private Vector3 playerpos = new Vector3(0, 0, 0);//ehm bunu ben ekledim :p -goktug

    // Start is called before the first frame update
    void Start()
    {
        playerpos.y = player.transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerpos.y < (player.transform.position.y + 1f))
        {
            playerpos.y = player.transform.position.y;
        }
        Vector3 newPosition = new Vector3(transform.position.x, playerpos.y + offset.y, transform.position.z);
        transform.position = newPosition;
        
    }
}
