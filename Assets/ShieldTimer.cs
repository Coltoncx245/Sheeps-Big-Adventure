using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldTimer : MonoBehaviour
{
    public Image shieldTimerImage;
    public GameObject player;
    private Shoot shootScript;


    void Awake()
    {
        shootScript = player.GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        shieldTimerImage.fillAmount = Mathf.Clamp((shootScript.shieldedTime - shootScript.shieldCurrentTime) / shootScript.shieldedTime, 0, 1f);
    }  
}
