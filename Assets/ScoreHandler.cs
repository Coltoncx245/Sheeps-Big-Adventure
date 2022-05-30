using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    public GameObject textObj;
    private TMP_Text scoreText;
    public float score;
    public float shotsFired;
    public float targetsHit;
    public float enemiesHit;

    void Awake()
    {
        textObj = GameObject.Find("Score");
        scoreText = textObj.GetComponent<TMP_Text>();
        score = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateScore(float s)
    {
        score += s;
    }

    public void UpdateShotsFired(int s)
    {
        shotsFired += s;
    }
    public void UpdateTargetsHit(int t)
    {
        targetsHit += t;
    }
    public void UpdateEnemiesHit(int e)
    {
        enemiesHit += e;
    }
}
