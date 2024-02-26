using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circles : MonoBehaviour
{
    public GameManage game;
    

    public bool gorevBasladi = false;

    public AudioSource ates;
    public AudioSource kapanma;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        ates.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (gorevBasladi)
        {
            gameObject.SetActive(true);
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Butterfly")
        {
            StartCoroutine(Bekle1(1f, 0));
        }
    }
    IEnumerator Bekle1(float waitTime, int gorev)
    {   
        kapanma.Play();

        yield return new WaitForSeconds(waitTime);

        if (gorev == 0)
        {
            ates.Stop();
            
            

            gameObject.SetActive(false);
            game.cemberSayisi += 1;
        }



    }
}
