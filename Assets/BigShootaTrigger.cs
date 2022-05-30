using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShootaTrigger : MonoBehaviour
{
    public GameObject player;
    
    

    void Start()
    {
        player = GameObject.Find("nose");
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Player")
        {
            player.SendMessage("BigShootin");
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }
}
