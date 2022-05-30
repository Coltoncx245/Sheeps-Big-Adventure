using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public GameObject textObj;
    private TMP_Text timeText;
    public Stopwatch gameSW = new Stopwatch();
    private int gameTime;


    void Awake()
    {
        textObj = GameObject.Find("Time");
        timeText = textObj.GetComponent<TMP_Text>();
        gameTime = 0;
    }


    void Start()
    {
        gameSW.Start();
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = (int)gameSW.Count();
        timeText.text = gameTime.ToString();
    }
}
