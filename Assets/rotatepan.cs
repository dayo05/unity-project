using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class rotatepan : MonoBehaviour
{
    public float a=0;
    public float r=0;
    private float speed = 3f;
    private Rigidbody rig;
    public SerialIO serialIO;
    private float GetPitch => serialIO.latest.pitch;
    private float GetYaw => serialIO.latest.yaw;
    private void Start()
    {
        serialIO = GameObject.Find("SerialIO").GetComponent<SerialIO>();
        rig = GetComponent<Rigidbody>();
    }

    void Move(){
        Vector3 mousePos = Input.mousePosition;
        float width=Screen.width/2;
        float height=Screen.height/2;
        r=90 * (mousePos.y-height) / height;
        a=90 * (-mousePos.x+width) / width;
        //rig.MoveRotation(Quaternion.Euler(new Vector3(-1*GetYaw, 0, GetPitch)));
        rig.MoveRotation(Quaternion.Euler(new Vector3(r,0,a)));
        //transform.eulerAngles = new Vector3(r, 0, a);
    }

    private void FixedUpdate()
    {
        Move();
        //gameObject.transform.parent.transform.rotation = gameObject.transform.rotation;
    }
}
