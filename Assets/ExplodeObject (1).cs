using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeObject : MonoBehaviour
{
    public float collisionMultp;
    public AudioSource boxExplodeSFX;
    private ButterflyMovement butterflyMovementSc;
    public bool didExploded = false;
    private GameObject hitObject;
    private GameObject childObj;
    public float BrokenPartCollMultp;
    public GameObject kapat;
    public GameObject PatlayanYol;
    public GameObject PatlayanKopru;
    
    void Start()
    {
        butterflyMovementSc = GameObject.FindWithTag("Butterfly").GetComponent<ButterflyMovement>();
    }


    void Update()
    {
        hitObject = butterflyMovementSc.shootedObject;
        if (didExploded && gameObject == PatlayanYol)
        {
            kapat.SetActive(false);
        }
        if (didExploded && gameObject == PatlayanKopru)
        {
            kapat.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        Debug.Log(hitObject + ", " + gameObject);

        if(hitObject == gameObject & !didExploded)
        {
            Debug.Log("Explode object");

            boxExplodeSFX.PlayOneShot(boxExplodeSFX.clip);

            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());
            
            foreach (var childRbs in gameObject.GetComponentsInChildren<Rigidbody>())
            {
                childObj = childRbs.gameObject;
                if(childObj != gameObject)
                {
                    childObj.AddComponent<MeshCollider>();
                    childObj.GetComponent<MeshCollider>().convex = true;

                    //childObj.AddComponent<BrokenPartExplode>();
                    //childObj.GetComponent<BrokenPartExplode>().collisionMultp = BrokenPartCollMultp;

                    childRbs.isKinematic = false;
                    childRbs.AddExplosionForce(collisionMultp, butterflyMovementSc.hit.point, 2);
                }
            }

            didExploded = true;
        }
    }
}
