using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ball : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody characterRigidbody;
    public GameObject Bomb;

    private AudioSource theAudio;
    public AudioClip GameOverY;
    [SerializeField] private Canvas gameOverCanvas;
 
    void Start(){
        characterRigidbody = GetComponent<Rigidbody>();
        theAudio = GetComponent<AudioSource>();
        Debug.Log(characterRigidbody.gameObject.transform.position);
        gameOverCanvas.enabled = false;
    }
 
    void Update(){
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        // -1 ~ 1
        Vector3 pos = transform.position;
        if(pos.y<-10){
            transform.position=new Vector3(0,10,0);
            characterRigidbody.velocity=new Vector3(0,0,0);
            gameOverCanvas.enabled = true;
            theAudio.PlayOneShot(GameOverY);
            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        gameOverCanvas.enabled = false;
        Time.timeScale = 1;
    }
    void OnCollisionEnter(Collision bomb) {
        if(bomb.collider.gameObject.CompareTag("bomb")){
            Vector3 force = transform.position - Bomb.transform.position + new Vector3(0, 0.5f, 0);
            force = Vector3.Normalize(force);
            Debug.Log(force);
            characterRigidbody.AddForce(force * 20, ForceMode.Impulse); // dir : 날리고싶은 방향
        }
    }
}