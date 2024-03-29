using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    private void OnMouseDown() {

         Vector3 dir= Camera.main.ScreenToViewportPoint(Input.mousePosition);
        dir.x= Mathf.Round(dir.x);
        dir.y= Mathf.Round(dir.y);
        dir.z= Mathf.Round(transform.position.z);
        Instantiate(prefab,dir,Quaternion.identity);
    }
    public void CreateObject( )
    {
       

    }
}
