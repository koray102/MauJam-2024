using UnityEngine;

public class CarMove : MonoBehaviour
{
    public Transform[] Waypoints1; // Array of waypoints defining the primary path
    public Transform[] Waypoints2; // Array of waypoints defining the alternate path
    public Transform[] Waypoints3; // Array of waypoints defining the alternate path
    public Transform[] Waypoints4; // Array of waypoints defining the alternate path
    public Transform[] Waypoints5; // Array of waypoints defining the alternate path
    public Transform[] Waypoints6; // Array of waypoints defining the alternate path
    public float speed = 5f; // Speed of the car
    public bool changePath = false; // Boolean to trigger path change
    public int changeNumber;

    private Transform[] currentWaypoints; // Current array of waypoints
    private int currentWaypointIndex = 0; // Index of the current waypoint

    private void Start()
    {
        // Move the car to the first waypoint of the primary path
        currentWaypoints = Waypoints1;
        transform.position = Waypoints1[0].position;
    }

    private void Update()
    {
        // Check if the car has reached the current waypoint
        if (Vector3.Distance(transform.position, currentWaypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex++;
            transform.LookAt(currentWaypoints[currentWaypointIndex].position);
            // If we've reached the end of the path, loop back to the beginning
            if (currentWaypointIndex >= currentWaypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        // Move the car towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoints[currentWaypointIndex].position, speed * Time.deltaTime);

        // Check if path change is triggered
        if (changePath)
        {
            // Call a method to change the path
            ChangePath();
        }
    }

    private void ChangePath()
    {
        // Toggle between primary and alternate paths
        if (currentWaypoints == Waypoints1 & changeNumber==2)
        {
            currentWaypoints = Waypoints2;
        }
       else if (currentWaypoints == Waypoints1 & changeNumber == 3)
        {
            currentWaypoints = Waypoints3;
        }
        else if (currentWaypoints == Waypoints1 & changeNumber == 4)
        {
            currentWaypoints = Waypoints4;
        }
        else if (currentWaypoints == Waypoints1 & changeNumber == 5)
        {
            currentWaypoints = Waypoints5;
        }
        else if (currentWaypoints == Waypoints1 & changeNumber == 6)
        {
            currentWaypoints = Waypoints6;
        }
        else
        {
            currentWaypoints = Waypoints1;
        }

        // Reset waypoint index
        currentWaypointIndex = 0;

        // Reset the boolean trigger
        changePath = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("change2"))
        {
            changePath = true;
            changeNumber = 2;
        }
        if (other.gameObject.CompareTag("change3"))
        {
            changePath = true;
            changeNumber = 3;
        }
        if (other.gameObject.CompareTag("change4"))
        {
            changePath = true;
            changeNumber = 4;
        }
        if (other.gameObject.CompareTag("change5"))
        {
            changePath = true;
            changeNumber = 5;
        }
        if (other.gameObject.CompareTag("change6"))
        {
            changePath = true;
            changeNumber = 6;
        }
    }
}
