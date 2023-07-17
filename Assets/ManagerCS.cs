using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ManagerCS : MonoBehaviour
{
    public int Score = 0;
    private List<bomb> Booms = new();
    [SerializeField] private Canvas gameOverCanvas;

    public Text myScore;
    
    private AudioSource theAudio;
    public AudioClip GameOverY;
    private void Start()
    {
        myScore=GameObject.Find("score").GetComponent<Text>();
        gameOverCanvas.enabled = false;
        theAudio = GetComponent<AudioSource>();
    }

    public void DestroyBoom(bomb boomObject)
    {
        Booms.Remove(boomObject);
        Destroy(boomObject.gameObject);
    }

    public bool IsBoom(bomb boomObject)
    {
        return Booms.Contains(boomObject);
    }

    private void ExplodeBoom(bomb target)
    {
        RegenBoom();
        Destroy(target.gameObject);
    }
    
    public bomb RegenBoom()
    {
        var boom = Instantiate(Bomb, Pos, Quaternion.identity); // Why this params required?? We'll going to rearrange the location maybe
        var bomb = boom.GetComponent<bomb>();
        Booms.Add(bomb);
        return bomb;
    }

    public void GameOver()
    {
        gameOverCanvas.enabled = true;
        theAudio.PlayOneShot(GameOverY);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Score = 0;
        myScore.text = Score.ToString();
        gameOverCanvas.enabled = false;
        Time.timeScale = 1;
    }

    public void printScore()
    {
        Score += 1;
        myScore.text = Score.ToString();
    }
}
