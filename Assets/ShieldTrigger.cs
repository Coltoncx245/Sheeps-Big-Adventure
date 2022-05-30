using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrigger : MonoBehaviour
{
    public GameObject player;

    void Awake()
    {
        player = GameObject.Find("nose");
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            player.SendMessage("Shielded");
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }
}
