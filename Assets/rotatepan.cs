using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatepan : MonoBehaviour
{
    public float a=0;
    public float r=0;
    private float speed = 3f;
    void Update(){
        Invoke("Move",0.1f);
    }
    void Move(){
        Vector3 mousePos = Input.mousePosition;
        float width=Screen.width/2;
        float height=Screen.height/2;
        r=90 * (mousePos.y-height) / height;
        a=90 * (-mousePos.x+width) / width;
        transform.eulerAngles = new Vector3(r, 0, a);
    }
}
