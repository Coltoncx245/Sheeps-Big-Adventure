using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Handles all independent functionality of NPC AI based on specific behavior states
 * which can be dictated by EventHandler or object interactions.
 * Contains function for checking whether the player is visible to the AI.
 */


public enum MenuBehaviorState { none, wander, gotopoint, gotoplayer, followPlayer, dead }

public class MenuAI : MonoBehaviour
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
    public GameObject eyes; // Empty object on NPC body to adjust for height/angle of view

    public float followTimer = 7000; // Amount of time to elapse before NPC stops following player in BehaviorState.followPlayer

    private Rigidbody rb;
    public float responseForce = 5;
    private CharacterController controller;
    private GameObject player;

    public Timer decayTimer;
    private bool isHit = false;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }


    void Start()
    {
        SetState(initialState);
        agent = GetComponent<NavMeshAgent>();



        rb = GetComponent<Rigidbody>();



    }

    // Update is called once per frame
    void Update()
    {


        Decay();



        if (currentState == BehaviorState.wander)
        {// NPC travels to randomly generated points and waits at each point for a random interval of time
        
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





    void Decay()
    {
        decayTimer.Start(30);
        if (decayTimer.Done())
        {
            Destroy(gameObject);
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



