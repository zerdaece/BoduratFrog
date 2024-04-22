using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBGstars : MonoBehaviour
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
            transform.GetComponent<backgrounhandle>().enabled = true;
        }
    }
}
