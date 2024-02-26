using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButterflyMovement : MonoBehaviour
{
    [Header("Butterfly")]
    public float transitionDelay;
    public Transform lookDirTransform;
    private Rigidbody btrflyRb;

    [Header("Speed")]
    public float forceInput;
    public float maxSpeed;
    private Vector3 btrflyVelo;
    private float force;
    private bool leftClick;
    
    [Header("Speed Power Up")]
    public Animator butterflyAnim;
    public ParticleSystem powerFocusVFX, powerVFX;
    public float powerFocusTime, powerDuration, powerCooldown, finishDashDelay;
    public Slider powerFocusTimeSlider, powerCooldownSlider;
    public GameObject powerFocusTimeSliderObj;
    public GameObject camHolder;
    public AudioSource dashSFX;
    internal bool isDashStarted;
    private bool isPowerUsed;
    private bool isForcePowerFinished = true;
    private bool spaceInput;
    private bool isCharged;
    private float spaceTimeCounter;
    private float powerCooldownCounter;

    [Header("Grab")]
    public float grabDelay;
    public float maxGrabDist, minGrabDist;
    public Transform camTransform;
    public LayerMask IgnoreRayCast;
    public Image chargeCrossImage, cooldownCrossImage;
    public Color crossHairColor, crossHairColorGrab;
    public AudioSource grabSFX, dropSFX;
    private bool didGrabSFXPlayed;
    private GameObject hitObject;
    private bool rightClick, rightClickUp;
    private bool isHold;
    private Rigidbody holdObjRb;
    private Vector3 movePosLerp;
    private Ray r;
    private float firstHitDistance;
    private float holdObjPos;

    [Header("Explode")] 
    internal GameObject shootedObject;
    internal RaycastHit hit;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        btrflyRb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        spaceInput = Input.GetKey(KeyCode.Space);
        leftClick = Input.GetMouseButton(0);
        rightClick = Input.GetMouseButton(1);
        rightClickUp = Input.GetMouseButtonUp(1);

        r = new Ray( origin: camTransform.position, direction: camTransform.forward);

        if(Physics.Raycast(camTransform.localPosition, camTransform.forward, out hit, maxGrabDist * 2, ~IgnoreRayCast))
        {
            Debug.Log("Touched any object");

            hitObject = hit.collider.gameObject;
            
            if(hitObject.CompareTag("Grab"))
            {
                Debug.Log("Touched grab object");

                chargeCrossImage.color = crossHairColorGrab;
                cooldownCrossImage.color = crossHairColorGrab;

                if(rightClick && !isHold)
                {
                    grabSFX.PlayOneShot(grabSFX.clip);
                    didGrabSFXPlayed = true;

                    movePosLerp = hitObject.transform.position;
                    firstHitDistance = hit.distance;
                    holdObjRb = hitObject.GetComponent<Rigidbody>();
                    holdObjRb.useGravity = false;
                    isHold = true;
                    holdObjPos = Mathf.Clamp(firstHitDistance, minGrabDist, maxGrabDist);
                }
            }
        }else if(!isHold && !rightClick)
        {
            chargeCrossImage.color = crossHairColor;
            cooldownCrossImage.color = crossHairColor;
        }

        if(spaceInput && !isPowerUsed)
        {
            spaceTimeCounter += Time.deltaTime;
        }else
        {
            spaceTimeCounter = 0;
            powerCooldownCounter += Time.deltaTime;
        }
        
        if(spaceTimeCounter > powerFocusTime || isCharged)
        {
            isForcePowerFinished = false;
            powerCooldownCounter = 0;
            isCharged = true;
            camHolder.transform.localEulerAngles = new Vector3(0, 0, 0);

            if(Input.GetKeyUp(KeyCode.Space))
            {
                force = forceInput * 10;
                StartCoroutine(PowerDelay());
                butterflyAnim.SetBool("Dash", true);
                
                if(powerVFX.isStopped)
                {
                    Debug.Log("Dash started");
                    isDashStarted = true;

                    powerFocusVFX.Stop();
                    powerVFX.Play();
                    dashSFX.PlayOneShot(dashSFX.clip);
                }
            }

        }else if (spaceTimeCounter > 0)
        {
            force = forceInput / 5;

            if(powerFocusVFX.isStopped)
            {
                powerFocusVFX.Play();
            }

        }else if(isForcePowerFinished)
        {
            force = forceInput;
            powerFocusVFX.Stop();
        }

        if(spaceTimeCounter == 0)
        {
            powerFocusTimeSliderObj.SetActive(false);
        }else
        {
            powerFocusTimeSliderObj.SetActive(true);
            powerFocusTimeSlider.value = spaceTimeCounter / powerFocusTime;
        }
        powerCooldownSlider.value = -powerCooldownCounter / (powerCooldown - powerDuration);
    }


    private void FixedUpdate()
    {
        if(leftClick)
        {
            btrflyRb.AddForce(lookDirTransform.forward * force);
        }

        btrflyRb.velocity = Vector3.ClampMagnitude(btrflyRb.velocity, maxSpeed);

        btrflyVelo = btrflyRb.velocity;
        transform.forward += Vector3.Lerp(transform.forward, btrflyVelo, transitionDelay);

        if(holdObjRb != null)
        {
            if(isHold && rightClick)
            {
                Debug.Log("Object was held. Pos: " + holdObjPos);

                movePosLerp = Vector3.Lerp(movePosLerp, r.GetPoint(holdObjPos), grabDelay);
                holdObjRb.MovePosition(movePosLerp);

                chargeCrossImage.color = crossHairColorGrab;
                cooldownCrossImage.color = crossHairColorGrab;
            }else
            {
                Debug.Log("Object was dropped.");
                
                if(didGrabSFXPlayed)
                {
                    dropSFX.PlayOneShot(grabSFX.clip);
                    didGrabSFXPlayed = false;
                }

                holdObjRb.useGravity = true;
                isHold = false;

                chargeCrossImage.color = crossHairColor;
                cooldownCrossImage.color = crossHairColor;
            }
        }
    }


    IEnumerator PowerDelay()
    {
        isPowerUsed = true;
        isCharged = false;

        yield return new WaitForSeconds(powerDuration);
        Debug.Log("Dash finished");

        isForcePowerFinished = true;
        butterflyAnim.SetBool("Dash", false);
        powerVFX.Stop();
        camHolder.transform.localEulerAngles = new Vector3(-15, 0, 0);
        holdObjPos = Mathf.Clamp(holdObjPos, minGrabDist, maxGrabDist);

        yield return new WaitForSeconds(finishDashDelay);
        isDashStarted = false;

        yield return new WaitForSeconds(powerCooldown - (powerDuration + finishDashDelay));
        isPowerUsed = false;
    }


    void OnCollisionEnter(Collision other)
    {
        Debug.Log("On collision enter");
        if(isDashStarted)
        {
            shootedObject = other.gameObject;
            Debug.Log(hitObject);
        }
    }
}
