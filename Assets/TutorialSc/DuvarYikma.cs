using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuvarYikma : MonoBehaviour
{
    public GameManage game;

    public AudioSource puff;
    public ParticleSystem puffGorsel;
    
    // Start is called before the first frame update
    void Start()
    {
        puff.Play();
        puffGorsel.gameObject.SetActive(true);
        puffGorsel.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Basla()
    {

    }
}
