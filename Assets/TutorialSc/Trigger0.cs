using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger0 : MonoBehaviour
{
    public GameObject cop;
    public GameObject kova;
    public AudioSource konusma1;
    public AudioSource konusma2;

    public Text textObject;
    // Start is called before the first frame update
    void Start()
    {
        cop.gameObject.SetActive(false);
        kova.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        textObject.text = "Objeleri tutmak icin Sag butonunu kullan";
        konusma1.Stop();
        konusma2.Play();
        cop.gameObject.SetActive(true);
        kova.gameObject.SetActive(true);
        gameObject.SetActive(false);    
    }
}
