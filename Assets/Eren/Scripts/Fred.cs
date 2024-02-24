using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fred : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints
    public float speed = 3.0f; // Movement speed
    public float waitTime = 2f; // Time to wait at each waypoint
    public Animator animator; // Reference to the animator component

    private int currentWaypointIndex = 0;
    private bool isWaiting = false;
    private Vector3 targetPosition;
    public bool BrokenGlass;
    public bool StopPath = false;
    public Transform glass;
    public Transform chair;

    void Start()
    {
        animator = GetComponent<Animator>();

        MoveToNextWaypoint();
    }

    void Update()
    {
        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        // Move towards the current waypoint
        if (!isWaiting && !StopPath)
        {
            float distance = Vector3.Distance(transform.position, targetPosition);
            if (distance > 0.1f)
            {

                transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime / distance);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            }
            else
            {
                StartCoroutine(WaitAtWaypoint());
            }
        }
        if (BrokenGlass)
        {
            GoToGlass();
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
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        targetPosition = waypoints[currentWaypointIndex].position;
        animator.SetBool("idle", false); // Set walking animation to true
    }
    public void GoToGlass()
    {
        StopPath = true;

        animator.SetBool("idle", false);
        targetPosition = glass.position;
        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        float distance = Vector3.Distance(transform.position, targetPosition);



        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime / distance);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

    }
    public void GoToSitting()
    {
        animator.SetBool("idle", false);
        targetPosition = chair.position;
        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        float distance = Vector3.Distance(transform.position, targetPosition);



        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime / distance);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Glass"))
        {
            Debug.Log("yarrak");
            animator.SetTrigger("crouch");
        }

        if (other.gameObject.CompareTag("Chair"))
        {
            animator.SetTrigger("sit");
        }
    }

}