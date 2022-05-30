using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Handles all independent functionality of NPC AI based on specific behavior states
 * which can be dictated by EventHandler or object interactions.
 * Contains function for checking whether the player is visible to the AI.
 */


public enum BehaviorState { none, wander, gotopoint, gotoplayer, followPlayer, dead }

public class AIMotor : MonoBehaviour
{
    public Timer t;
    
    public BehaviorState initialState;
    public BehaviorState currentState = BehaviorState.none;

    [Header("Wander Settings")]
    public Bounds boundBox;
    private NavMeshAgent agent;
    private Vector3 targetPos;
    
    private Transform playerTransform; 
   
    public float waitTime;
    public float lookRadius;

    private Camera playerCamera;
    public RaycastHit hit;
    public GameObject waypoint;
    // public GameObject eyes; // Empty object on NPC body to adjust for height/angle of view

    public float followTimer = 7000; // Amount of time to elapse before NPC stops following player in BehaviorState.followPlayer

    private Rigidbody rb;
    public float responseForce = 5;
    private CharacterController controller;
    private GameObject player;

    public Timer decayTimer;
    private bool isHit = false;
    private GameObject gh;

    public AudioSource deathSound;
    


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gh = GameObject.Find("GameHandler");
    }


    void Start()
    {
        SetState(initialState);
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Third Person Player");
        Transform playerTransform = player.transform;

        rb = GetComponent<Rigidbody>();

        controller = player.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        
        Decay();
        
        

        if (currentState == BehaviorState.wander) 
        {// NPC travels to randomly generated points and waits at each point for a random interval of time
            if (CanSeePlayer())
            {
                currentState = BehaviorState.followPlayer;
            }
            float targetDistance = Vector3.Distance(targetPos, transform.position);
            if (targetDistance <= agent.stoppingDistance)
            {

                t.Start(waitTime); // Wait for time determined in FindNewWanderTarget()
                if (t.Done())
                {
                    agent.isStopped = false;
                    FindNewWanderTarget();
                    t.Reset();
                }
                else
                {
                    agent.isStopped = true;
                }
               
                
            }
        }

        if (currentState == BehaviorState.gotopoint)
        {// NPC travels to specified point
            agent.SetDestination(waypoint.transform.position);
        }

        if (currentState == BehaviorState.followPlayer)
        {/* NPC will follow player if it can see them. Each time the player becomes visible to the NPC 
          * the followTimer will reset to it's max value. If the followTimer reaches 0 without the NPC 
          * seeing the player again, the NPC will remain stationary until the player becomes visible 
          * again.
          */



            if (CanSeePlayer())
            {
                agent.SetDestination(player.transform.position);
                followTimer = 10000;
            }

            else if (CanSeePlayer() == false && followTimer >= 0 )
            {
                agent.SetDestination(player.transform.position);
                followTimer--;
            }

            else
            {
                currentState = BehaviorState.wander;
            }
        }

        if (currentState == BehaviorState.gotoplayer)
        {// NPC will go to the player's position, regardless of if they are visible

            agent.SetDestination(player.transform.position);
        }

        if (currentState == BehaviorState.dead)
        {
            agent.enabled = false;

        }
    }

    public void SetState(BehaviorState s)
    {
        if (currentState != s)
        {
            currentState = s;
            if (currentState == BehaviorState.wander) // Refreshes wander target if SetState is called from elsewhere
            {
                FindNewWanderTarget();
            }
        }

    }

    void FindNewWanderTarget()
    {
        waitTime = Random.Range(2, 5); // Determine new wait time
        targetPos = GetRandomPoint();

        
        agent.isStopped = false;
        agent.SetDestination(targetPos);

    }

    Vector3 GetRandomPoint()
    {
        float randomX = Random.Range(-boundBox.extents.x + agent.radius + 1, boundBox.extents.x - agent.radius - 1);
        float randomZ = Random.Range(-boundBox.extents.z + agent.radius + 1, boundBox.extents.z - agent.radius - 1);
        return new Vector3(randomX, transform.position.y, randomZ);

    }

    private bool CanSeePlayer()
    {

        Vector3 rayDir = transform.position - playerCamera.transform.position; // Direction from eyes obj to player camera

        if (Physics.Raycast(playerCamera.transform.position, rayDir, out hit))
        {
            Debug.DrawRay(playerCamera.transform.position, rayDir); // Visible ray for debugging in scene view

            if (hit.transform.name == transform.name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    // Collision Handler
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Projectile")
        {
            if (c.gameObject.name == "BigAmmo")
            {
                responseForce = 5000;
            }

            if (!isHit)
            {
                deathSound.Play();
                SetState(BehaviorState.dead);
                rb.AddForce(c.relativeVelocity * responseForce);
                isHit = true;
                gh.SendMessage("UpdateScore", 15);
                gh.SendMessage("UpdateEnemiesHit", 1);
            }
            

            
        }
        else if (c.gameObject.tag == "Target")
        {
            if (!isHit)
            {
                deathSound.Play();
                SetState(BehaviorState.dead);
                rb.AddForce(c.relativeVelocity * responseForce);
                isHit = true;
                print("TRICK SHOT");
                gh.SendMessage("UpdateScore", 20);
                
            }
            
        }
        /*
        else if (c.gameObject.tag == "Enemy")
        {
            
            if (!isHit)
            {
                deathSound.Play();
                SetState(BehaviorState.dead);
                rb.AddForce(c.relativeVelocity * responseForce);
                isHit = true;
                print("DOMINOES");
                gh.SendMessage("UpdateScore", 25);
            }
        }
        */
        else if (c.gameObject.tag == "Player")
        {
            if (!isHit)
            {
                player.SendMessage("Dead");
            }
            
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPos, 0.2f);
    }

  

}
