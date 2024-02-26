using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeMachine : MonoBehaviour
{
    public Fred Fred;
    public ParticleSystem fire;
    public AudioSource fireSound;
   


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fireable"))
        {
            
            Fred.Fire = true;
            fire.Play();
            
            fireSound.Play();
            gameObject.SetActive(false);
        }
    }
}
   
