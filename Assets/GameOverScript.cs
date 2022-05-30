using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    public Animation anim;
    private GameObject canv;
    public GameObject player;
    public Timer t;
    private bool timerStarted = false;
    public AudioSource gameOverSound;
    private int shotsFired;
    public GameObject nose;
    

    // Start is called before the first frame update
    
    void Awake()
    {
       
        canv = GameObject.Find("Canvas");
        

    }
    
    void Start()
    {
        canv.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            if (t.Done())
            {
                player.SetActive(false);
            }
        }
        
        
    }

    public void GameOver()
    {
        t.Start(2);
        gameOverSound.Play();
        canv.SetActive(true);
        anim.Play("Game Over Overlay");
        anim.Play("Game Over Text");
        Cursor.visible = true;
        Screen.lockCursor = false;
        timerStarted = true;
        shotsFired = nose.GetComponent<Shoot>().shotsFired;
        print(shotsFired);
        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameQuit()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
