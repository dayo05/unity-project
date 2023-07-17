using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorezon : MonoBehaviour
{
    public GameObject zone;
    private ManagerCS manager;
    private Vector3 stat;

    private Vector3 pos;
    
    public SerialIO serialIO;
    private float GetPitch => serialIO.latest.pitch;
    private float GetYaw => serialIO.latest.yaw;
    // Start is called before the first frame update
    void Start()
    {
        serialIO = SerialIO.Obtain();
        manager=GameObject.Find("Manager").GetComponent<ManagerCS>();
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Rotate_Ang(-1*GetPitch*Mathf.PI/180, GetYaw*Mathf.PI/180,
            new Vector3(pos.x,0,pos.z));
        transform.eulerAngles = new Vector3(GetYaw, 0, -1*GetPitch);
    }
    
    private void Awake()
    {
    }
    
    Vector3 Return_RandomPosition()
    {
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        /*float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;*/
        
        float rangeX = Random.Range( -8.5f,8.5f);
        float rangeZ = Random.Range( -6.5f,6.5f);
        Vector3 RandomPostion = new Vector3(rangeX, 0,rangeZ);
        stat = RandomPostion;
        if(serialIO == null) Debug.LogError("STAT IS NULL");
        Vector3 respawnPosition = Rotate_Ang(-1*GetPitch*Mathf.PI/180, GetYaw*Mathf.PI/180,stat); //rothang()
        return respawnPosition;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.gameObject.CompareTag("Sphere")) return;
        transform.position = Return_RandomPosition();
        pos = transform.position;
        manager.printScore();
    }

    //public SerialIO serialIO;
    //private double GetPitch => serialIO.latest.pitch;
    //private double GetYaw => serialIO.latest.yaw;
    private Vector3 Rotate_Ang(float Pitch, float Yaw,Vector3 pos){
        float ca=Mathf.Cos(Pitch);
        float cr=Mathf.Cos(Yaw);
        float sa=Mathf.Sin(Pitch);
        float sr=Mathf.Sin(Yaw);
        float x = ca*pos.x - sa*cr*pos.y + sa*sr*pos.z;
        float y = sa*pos.x + ca*cr*pos.y - sr*ca*pos.z;
        float z = sr*pos.y + cr*pos.z;
        return new Vector3(x,y,z);
    }
}
