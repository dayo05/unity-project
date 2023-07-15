using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    private Transform myTransform=null;
    public GameObject Explosion=null;
    Material material;
    Color color;
    public GameObject Bomb;

    private AudioSource theaudio;
    public AudioClip ExplodeSound;

    private Vector3 stat;
    public SerialIO serialIO;
    private float GetPitch => serialIO.latest.pitch;
    private float GetYaw => serialIO.latest.yaw;
    // Start is called before the first frame update
    void Start()
    {
        myTransform=GetComponent<Transform>();
        material = gameObject.GetComponentInChildren<MeshRenderer>().material;
        theaudio = GetComponent<AudioSource>();
        color=material.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(-1 * GetPitch, 0, GetYaw);
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.gameObject.CompareTag("Sphere")){
            material.color=new Color32(250, 2, 2,255);
            Invoke("explode",0.25f);
    }
}
    private void explode(){
        Instantiate(Explosion, myTransform.position, Quaternion.identity);
        theaudio.PlayOneShot(ExplodeSound);
        material.color=color;
        Instantiate(Bomb, Return_RandomPosition(), Quaternion.identity);
    }
    Vector3 Return_RandomPosition()
    {
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        /*float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;*/
        
        float rangeX = Random.Range( -8.5f,8.5f);
        float rangeZ = Random.Range( -6.5f,6.5f);
        Vector3 randomPostion = new Vector3(rangeX, 0,rangeZ);
        stat = randomPostion;
        Vector3 respawnPosition = Rotate_Ang(); //rothang()
        return respawnPosition;
    }
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
