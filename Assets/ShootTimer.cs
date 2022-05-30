using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootTimer : MonoBehaviour
{
    public Image shootTimerImage;
    public GameObject player;
    private Shoot shootScript;


    void Awake()
    {
        shootScript = player.GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimerImage.fillAmount = Mathf.Clamp((shootScript.bigShootaTime - shootScript.shootCurrentTime) / shootScript.bigShootaTime, 0, 1f);
    }
}
