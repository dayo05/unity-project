using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorezon : MonoBehaviour
{
    public GameObject zone;
    public GameObject parent;
    public Text myScore;
    private GameObject S;
    private int s;
    private GameObject ra;
    private Vector3 stat;
    
    public SerialIO serialIO;
    private float GetPitch => serialIO.latest.pitch;
    private float GetYaw => serialIO.latest.yaw;
    // Start is called before the first frame update
    void Start()
    {
        myScore=GameObject.Find("score").GetComponent<Text>();
        S=GameObject.Find("score");
        ra=GameObject.Find("rotating");
        Vector3 g=Return_RandomPosition();
        zone.transform.position=g;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(-1 * GetPitch, 0, GetYaw);
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
        Vector3 respawnPosition = Rotate_Ang(); //rothang()
        return respawnPosition;
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.collider.gameObject.CompareTag("Sphere")){
            Instantiate(zone,Return_RandomPosition(),Quaternion.identity).transform.parent=parent.transform;
            S.GetComponent<score>().Score+=1;
            printScore();
            Destroy(zone);
        }
    }
    private void printScore(){
        myScore.text=S.GetComponent<score>().Score.ToString();
    }

    //public SerialIO serialIO;
    //private double GetPitch => serialIO.latest.pitch;
    //private double GetYaw => serialIO.latest.yaw;
    private Vector3 Rotate_Ang(){
        float ca=Mathf.Cos(GetPitch);
        float cr=Mathf.Cos(GetYaw);
        float sa=Mathf.Sin(GetPitch);
        float sr=Mathf.Sin(GetYaw);
        float x = ca*stat.x - sa*cr*stat.y + sa*sr*stat.z;
        float y = sa*stat.x + ca*cr*stat.y - sr*ca*stat.z;
        float z = sr*stat.y + cr*stat.z;
        return new Vector3(x,y,z);
    }
}
