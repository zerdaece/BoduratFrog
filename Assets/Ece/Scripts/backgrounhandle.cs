using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class backgrounhandle : MonoBehaviour
{
    public GameObject bground;
    private GameObject bgroundobject;

    // Start is called before the first frame update

    // Update is called once per frame
    public void Update()
    {

        if (bgroundobject.transform.localPosition.y < transform.position.y - 10f)
        {
            Destroy(bgroundobject);
        }
        if (bgroundobject.transform.localPosition.y > 10f)
        {
            GameObject bgroundobjectobject = Instantiate(bgroundobject, transform.position, Quaternion.identity);
            bgroundobject.transform.parent = transform;
        }
    }
}

