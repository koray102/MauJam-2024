using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugFix : MonoBehaviour
{
    private Vector3 konum = new Vector3(-14.7700005f, 21.8099995f, -42.9230003f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Grab")
        {
            other.transform.position = konum;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Grab")
        {
            collision.transform.position = konum;
        }
    }
}
