using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorezone : MonoBehaviour
{
    public GameObject rangeObject;
    BoxCollider rangeCollider;
    public GameObject zone;
    public GameObject parent;
    public Text myScore;
    private GameObject S;
    private int s;
    private GameObject ra;
    private Vector3 stat;
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
        //Debug.Log(zone.transform.position);   
    }
    
    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }
    
    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;
        
        range_X = Random.Range( -8.5f,8.5f);
        range_Z = Random.Range( -6.5f,6.5f);
        Vector3 RandomPostion = new Vector3(range_X, 0,range_Z);
        stat = RandomPostion;
        Vector3 respawnPosition = stat;
        return respawnPosition;
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.collider.gameObject.CompareTag("Sphere")){
            Instantiate(zone,new Vector3(0,0.5f,0),Quaternion.identity).transform.parent=parent.transform;
            //S.GetComponent<score>().Score+=1;
            printScore();
            Destroy(zone);
        }
    }
    private void printScore(){
        //myScore.text=S.GetComponent<score>().Score.ToString();
    }
    // private Vector3 rothang(){
    //     float ca=Mathf.Cos(ra.GetComponent<rotatepan>().a*Mathf.PI/180);
    //     float cr=Mathf.Cos(ra.GetComponent<rotatepan>().r*Mathf.PI/180);
    //     float sa=Mathf.Sin(ra.GetComponent<rotatepan>().a*Mathf.PI/180);
    //     float sr=Mathf.Sin(ra.GetComponent<rotatepan>().r*Mathf.PI/180);
    //     float x = ca*stat.x - sa*cr*stat.y + sa*sr*stat.z;
    //     float y = sa*stat.x + ca*cr*stat.y - sr*ca*stat.z;
    //     float z = sr*stat.y + cr*stat.z;
    //     return new Vector3(x,y,z);
    // }
}
