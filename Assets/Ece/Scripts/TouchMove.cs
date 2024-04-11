using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
  

    // Update is called once per frame
    void Update()
    {
       if (Input.touchCount > 0)
       {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                Debug.Log("Tıklandı");
            }
            if(touch.phase == TouchPhase.Stationary)
            {
                Debug.Log("Dokunuyor");
            }
            if(touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Sürükleniyor");
                Debug.Log(touch.deltaPosition);
            }
            if(touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Tık bırakıldı");
            }
       } 
       
    }
}
