using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

public class bomb : MonoBehaviour
{
    private Transform myTransform = null;
    public GameObject Explosion = null;
    Material material;
    Color color;
    public GameObject Bomb;

    private AudioSource theaudio;
    public AudioClip ExplodeSound;

    private Vector3 stat;
    public SerialIO serialIO;
    private float GetPitch => serialIO.latest.pitch;
    private float GetYaw => serialIO.latest.yaw;

    private Vector3 Pos;

    // Start is called before the first frame update
    void Start()
    {
        serialIO = SerialIO.Obtain();
        myTransform = GetComponent<Transform>();
        material = gameObject.GetComponentInChildren<MeshRenderer>().material;
        theaudio = GetComponent<AudioSource>();
        color = material.color;
        Pos = transform.position;
        gameObject.tag = "bomb";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position = Rotate_Ang(-1 * GetPitch * Mathf.PI / 180, GetYaw * Mathf.PI / 180,
            new Vector3(Pos.x, 0, Pos.y));
        transform.eulerAngles = new Vector3(GetYaw, 0, -1 * GetPitch);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.gameObject.CompareTag("Sphere")) return;
        material.color = new Color32(250, 2, 2, 255);
        theaudio.PlayOneShot(ExplodeSound);
        Invoke(nameof(explode), 0.25f);
    }

    private void explode()
    {
        Instantiate(Explosion, myTransform.position, Quaternion.identity);
        material.color = color;
        Pos = Return_RandomPosition();
        Instantiate(Bomb, Pos, Quaternion.identity);
        Destroy(gameObject);
    }

    Vector3 Return_RandomPosition()
    {
        float rangeX = Random.Range(-6f, 6f);
        float rangeZ = Random.Range(-6f, 6f);
        Vector3 randomPostion = new Vector3(rangeX, 0, rangeZ);
        stat = randomPostion;
        Vector3 respawnPosition = Rotate_Ang(GetPitch * Mathf.PI / 180, GetYaw * Mathf.PI / 180, stat);
        return respawnPosition;
    }

    private Vector3 Rotate_Ang(float pitch, float yaw, Vector3 pos)
    {
        var ca = Mathf.Cos(pitch);
        var cr = Mathf.Cos(yaw);
        var sa = Mathf.Sin(pitch);
        var sr = Mathf.Sin(yaw);
        var x = ca * pos.x - sa * cr * pos.y + sa * sr * pos.z;
        var y = sa * pos.x + ca * cr * pos.y - sr * ca * pos.z;
        var z = sr * pos.y + cr * pos.z;
        return new Vector3(x, y, z);
    }
}
