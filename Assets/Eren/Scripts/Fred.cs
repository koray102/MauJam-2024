using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fred : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints
    public float waitTime = 2f; // Time to wait at each waypoint
    public Animator animator; // Reference to the animator component
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        // Check if the agent reached the destination
        if (!agent.pathPending && agent.remainingDistance < 0.1f && !isWaiting)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        animator.SetBool("idle", true); // Set walking animation to false
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        MoveToNextWaypoint();
    }

    void MoveToNextWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            // End of waypoints, do something (e.g., restart from the first waypoint)
            currentWaypointIndex = 0;
            return;
        }

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        animator.SetBool("idle", false); // Set walking animation to true
        currentWaypointIndex++;
    }
}