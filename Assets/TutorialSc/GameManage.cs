using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public Bin copGorevi;
    public Circles CemberGorevi1;
    public Circles CemberGorevi2;
    public Circles CemberGorevi3;
    public DuvarYikma duvarGorevi;
    public Elevator AsansorGorevi;

    public GameObject Elevator;
    public GameObject Cemberler;
    public GameObject duvar;


    public bool copGoreviTamam = false;
    public bool cemberGoreviTamam = false;
    public bool AsansoreBindi = false;
    public bool duvarGoreviTamam = false;

    public ExplodeObject DuvarYýkýlma;

    public AudioSource Muzik;
    public AudioSource konusma1;
    public AudioSource konusma2;
    public AudioSource konusma3;
    public AudioSource konusma4;
    public AudioSource konusma5;

    public Text textObject;
    
    public int cemberSayisi = 0;
    // Start is called before the first frame update
    void Start()
    {
        duvar.SetActive(false);
        Cemberler.SetActive(false);
        CopGoreviBaslat();
        konusma1.Play();

        textObject.text = "Koridoru gecmek icin farenin sol butonunu kullan";
    }

    // Update is called once per frame
    void Update()
    {
        if(copGoreviTamam)
        {
            textObject.text = "Dash Atmak icin SPACE basili tut ve Bar dolunca SOL BUTONUNA basili tutarken birak";
            konusma2.Stop();
            konusma3.Play();
            copGoreviTamam = false;
            CemberGoreviBasla();            

            CemberGorevi1.gorevBasladi = true;
            CemberGorevi2.gorevBasladi = true;
            CemberGorevi3.gorevBasladi = true;
        }
        if(cemberSayisi == 3)
        {
            cemberGoreviTamam = true;
            cemberSayisi = 14;
        }

        if (cemberGoreviTamam)
        {
            textObject.text = "Yapilari Yikmak icin Dash ile kafa at";
            konusma3.Stop();
            konusma4.Play();
            cemberGoreviTamam = false;

            duvarGoreviBasla();

        }
        if (duvarGoreviTamam)
        {
            duvarGoreviTamam = false;
            konusma4.Stop();
            konusma5.Play();

            AsansoruAc();
        }
        if (DuvarYýkýlma.didExploded)
        {
            DuvarYýkýlma.didExploded = false;
            duvarGoreviTamam = true;
        }
        
    }

    void CopGoreviBaslat()
    {

    }
    void CemberGoreviBasla() 
    {
        Cemberler.SetActive(true);
    }
    void AsansoruAc()
    {   
        AsansorGorevi.AsanSorSesiCalis();
        Elevator.transform.GetChild(0).gameObject.SetActive(false);
        Elevator.transform.GetChild(1).gameObject.SetActive(false);
        
    }
    void duvarGoreviBasla()
    {
        
        duvarGorevi.Basla();
        duvar.SetActive(true);
    }

}
