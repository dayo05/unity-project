using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    private Transform myTransform=null;
    public GameObject Explosion=null;
    Material material;
    Color color;
    private float Timer=0.0f;
    // Start is called before the first frame update
    void Start()
    {
        myTransform=GetComponent<Transform>();
        material = gameObject.GetComponentInChildren<MeshRenderer>().material;
        color=material.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.gameObject.CompareTag("Sphere")){
            material.color=new Color32(250, 2, 2,255);
            Invoke("explode",0.25f);
    }
}
    private void explode(){
        Instantiate(Explosion, myTransform.position, Quaternion.identity);
        material.color=color;
    }
}
