using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Fred : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints
    public float speed = 3.0f; // Movement speed
    public float waitTime; // Time to wait at each waypoint
    public Animator animator; // Reference to the animator component
    public trigger trigger;

    private int currentWaypointIndex = 0;
    private bool isWaiting = false;
    private Vector3 targetPosition;


    public bool StopPath = false;
    public GameObject glass;
    public Transform chair;
    public Transform fallpoint;
    public Transform firepoint;
    public Transform firend;
    public GameObject BrokeGlass;
    public GameObject fallingBucket;
    public GameObject CreatedBucket;
    public GameObject FireEnder;
    public GameObject FireEnderFred;
    public ParticleSystem fireParticle;
    public ParticleSystem firendParticle;
    public AudioSource fireEXT;
    public AudioSource fallSound;
    public AudioSource fireSound;
    public bool stopmusic=false;
    public bool sit = false;
    public bool fall = false;
    public bool BrokenGlass = false;
    public bool dontwalk = false;
    public bool paint = false;
    public bool Canpaint = false;
    public bool Fire = false;
    public bool FireEnd = false;
    public bool canCrouch = false;
    public bool iptal = false;



    void Start()
    {
        animator = GetComponent<Animator>();

        MoveToNextWaypoint();
    }

    void Update()
    {
        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        if (currentWaypointIndex == 6)
        {
            StopPath = true;
        }
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
        if (BrokenGlass & StopPath & !Fire)
        {
            GoToGlass();


        }
        if (sit & StopPath)
        {
            GoToSitting();

        }
        if (paint & StopPath & Canpaint)
        {
            GetPainted();
        }
        if (Fire & StopPath)
        {
            GoToFireEnder();
        }
        if (FireEnd & StopPath)
        {
            GoToMachine();
        }
        if (Fire & BrokenGlass & StopPath)
        {
            GoToFireEnder();
        }
        if (Canpaint)
        {
            CanPaint();
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.y, transform.rotation.z);
        animator.SetBool("idle", true); // Set walking animation to false
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        if (!StopPath)
        {
            MoveToNextWaypoint();
        }


    }

    void MoveToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1);
        targetPosition = waypoints[currentWaypointIndex].position;
        animator.SetBool("idle", false); // Set walking animation to true

    }

    public void GoToGlass()
    {
        canCrouch = true;
        
        
        animator.SetBool("idle", false);
        targetPosition = BrokeGlass.transform.position;
        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > .3f && !dontwalk & !FireEnd & canCrouch & !iptal)
        {   
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime / distance);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else if ( !FireEnd & canCrouch & !iptal)
        {
            animator.SetTrigger("crouch");
            animator.SetBool("idle", true);
            BrokenGlass = false;

        }
    }


    public void GoToSitting()
    {


        animator.SetBool("idle", false);

        targetPosition = chair.position;
        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        float distance = Vector3.Distance(transform.position, targetPosition);
        Debug.Log(distance);

        if (distance > .3f)
        {

            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime / distance);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else
        {

            animator.SetTrigger("sit");
            sit = false;
        }
    }
    public void booler()
    {
        sit = true;
    }



    public void CreateGlass(Transform transform1)
    {

        Instantiate(BrokeGlass, transform1.position, Quaternion.identity);
        glass.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("su") & fall)
        {
            dontwalk = true;
            animator.SetTrigger("fall");
            iptal = true;
            canCrouch = false;
            fallSound.Play();
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("su"))
        {
            iptal = false;

        }
    }
    public void OpenWalk()
    {
        dontwalk = false;
        animator.SetBool("idle", false);
    }
    public void CanPaint()
    {
        
       

    }
    public void GetPainted()
    {
        fallingBucket.SetActive(false);
        CreatedBucket.SetActive(true);
        animator.SetTrigger("bucket");
    }
    public void GoToFireEnder()
    {
        animator.SetBool("idle", false);

        targetPosition = firepoint.position;
        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        float distance = Vector3.Distance(transform.position, targetPosition);


        if (distance > .3f & !iptal)
        {
            animator.SetBool("idle", false);
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime / distance);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else if (!iptal)
        {
            animator.SetTrigger("pickup");


            Fire = false;

        }
    }
    public void TakeFire()
    {
        FireEnder.SetActive(false);
        FireEnderFred.SetActive(true);

        dontwalk = false;
        animator.SetBool("idle", false);
        
    }
    public void GoToMachine()
    {
        targetPosition = firend.position;
        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        float distance = Vector3.Distance(transform.position, targetPosition);


        if (distance > .7f & !dontwalk)
        {
            canCrouch = false;
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime / distance);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else
        {
            animator.SetTrigger("firend");
            firendParticle.Play();
            fireEXT.Play();
            FireEnd = false;
        }
    }
    public void FireEndTrigger()
    {
        FireEnd = true;
    }
    public void FirendAnim()
    {
        FireEnderFred.SetActive(false);
        fireParticle.Stop();
        firendParticle.Stop();
        fireEXT.Stop();
        fireSound.Stop();
        animator.SetBool("idle", true);
        canCrouch = true;
    }
    public void PaintBoolerOn()
    {
        Canpaint = true;
    }
    public void PaintBoolerOf()
    {
        Canpaint = false;
    }
    public void SceneGec()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}