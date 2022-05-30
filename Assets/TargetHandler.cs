using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    private Rigidbody rb;
    public float responseForce = 5;
    private CharacterController controller;
    private GameObject player;
    private GameObject gamehandler;
    private bool isHit = false;

    public AudioSource hitSound;

    public Timer decayTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Third Person Player");
        controller = player.GetComponent<CharacterController>();
        gamehandler = GameObject.Find("GameHandler");
        

    }

    // Update is called once per frame
    void Update()
    {
        Decay(); 
    }
    void OnCollisionEnter(Collision c)
    {
        hitSound.Play();
        if (c.gameObject.tag == "Projectile")
        {
            
            if (c.gameObject.name == "BigAmmo")
            {
                responseForce = 100;
            }

            isHit = true;
            rb.AddForce(c.relativeVelocity * responseForce);
            

            gamehandler.SendMessage("UpdateScore", 10);
            gamehandler.SendMessage("UpdateTargetsHit", 1);
        }
        else if (c.gameObject.tag == "Player")
        {

            isHit = true;
            print("Player Collision");
            rb.AddForce(controller.velocity * responseForce * 500);
            gamehandler.SendMessage("UpdateScore", 5);
        }
        
            
        
    }

    void Decay()
    {
        if (isHit)
        {

            decayTimer.Start(5);
            if (decayTimer.Done())
            {
                Destroy(gameObject);
            }

        }

    }
}

