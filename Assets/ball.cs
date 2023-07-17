using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ball : MonoBehaviour
{
    private Rigidbody characterRigidbody;

    private ManagerCS manager;
    private float gravity = 20;
    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        manager = GameObject.Find("Manager").GetComponent<ManagerCS>();
        
    }
 
    void FixedUpdate(){
        var pos = transform.position;
        characterRigidbody.AddForce(Vector3.down*gravity);
        if(pos.y<-10){
            transform.position=new Vector3(0,10,0);
            characterRigidbody.velocity=new Vector3(0,0,0);
            manager.GameOver();
        }
    }

    void OnCollisionEnter(Collision bomb)
    {
        if (!bomb.gameObject.CompareTag("bomb")) return;
        var force = transform.position - bomb.transform.position + new Vector3(0, 0.5f, 0);
        force = Vector3.Normalize(force);
        characterRigidbody.AddForce(force * 30, ForceMode.Impulse); // dir : 날리고싶은 방향
    }
}