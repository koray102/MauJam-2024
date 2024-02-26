using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kutu2script : MonoBehaviour
{
    public int sayi = 0;
    public GameObject kapat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sayi >= 2)
        {
            kapat.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            sayi++;
        }
    }
}
