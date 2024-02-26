using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    public GameManage oyun;
    public ParticleSystem patlama;
    public GameObject cop;
    public AudioSource sevinc;
    public AudioSource puff;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Grab")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            cop.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            sevinc.Play();

            StartCoroutine(Bekle(2f, 0));
        }
    }

    IEnumerator Bekle(float waitTime, int gorev)
    {
        yield return new WaitForSeconds(waitTime);

        if(gorev == 0)
        {
            puff.Play();
            oyun.copGoreviTamam = true;
            patlama.gameObject.SetActive(true);
            StartCoroutine(Bekle(1f, 1));
            
        }
        if(gorev == 1)
        {
            gameObject.SetActive(false);
        }

     

    }

}
