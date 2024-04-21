using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class StopCloudsPos : MonoBehaviour
{
    [SerializeField] int pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > pos)
        {
            transform.GetComponent<FollowPlayer>().enabled = false;
        }
    }
}
