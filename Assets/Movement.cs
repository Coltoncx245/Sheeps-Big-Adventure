using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public CharacterController controller;
    public float moveSpeed = 1f;
    public float turnSmoothTime = 0.5f;
    private float turnSmoothVelocity;
    private Animation anim;
    public float mouseXSensitivity = 1;
    private GameObject goHandler;
    public GameObject sheep;
    public GameObject nose;
    private bool isShielded;
    

    public Transform cam;

    public float speed = 25f;
    public float rotateSpeed = 2f;

    float gravity = 9.8f;
    float vSpeed = 0f;
    



    void Awake()
    {
           
        anim = sheep.GetComponent<Animation>();
        goHandler = GameObject.Find("GameHandler");


    }

  
    void Update()
    {
       
        isShielded = nose.GetComponent<Shoot>().shielded;


        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);

        vSpeed -= gravity * Time.deltaTime;
        forward.y = vSpeed; 
        controller.Move(forward * Time.deltaTime);


    }

    void OnControllerColliderHit(ControllerColliderHit h)
    {
        if (h.gameObject.name != "Floor")
        {
            // print("Controller Collision with " + h.gameObject.name);
           
            
        }
        
    }

    public void Dead()
    {
        if (isShielded)
        {
            print("Shielded!");
        }
        else
        {
            print("Dead");
            anim.Play("Sheep Death");
            goHandler.SendMessage("GameOver");
        }
        

    }


    
}
