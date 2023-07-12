using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ball : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody characterRigidbody;
 
    void Start(){
        characterRigidbody = GetComponent<Rigidbody>();
    }
 
    void Update(){
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        // -1 ~ 1
        Vector3 pos = transform.position;
        if(pos.y<-10){
            transform.position=new Vector3(0,10,0);
            characterRigidbody.velocity=new Vector3(0,0,0);
        }
    }
    private void OnCollisionEnter(Collision bomb) {
        if(bomb.collider.gameObject.CompareTag("bomb")){
            Debug.Log("yes");
            characterRigidbody.AddForce(new Vector3(10, 10, 10), ForceMode.Impulse); // dir : 날리고싶은 방향
        }
    }
}