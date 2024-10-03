using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Playables;

public class pickUpItem : MonoBehaviour
{

    private bool estAccessible = false;
    public PickUpAllow pickUp;
    private GameObject joueur;
    [SerializeField]
    private UnityEvent onPickUp;
    [SerializeField]
    private string messageDePickUp;
    [SerializeField]
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        estAccessible = false ;
    }

    // Update is called once per frame
    void Update()
    {
        joueur = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            estAccessible = true;
            GestionnaireUI.gestUI.AfficherIndications(messageDePickUp);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            estAccessible = false;
            GestionnaireUI.gestUI.RetirerIndications();
        }
    }

    private void FixedUpdate()
    {
        if (estAccessible && pickUp.PickupAllowed())
        {
            //Manquera de faire le code pour débarer la porte.
            onPickUp.Invoke(); //besoin du cote des autre pour fairer les autre code code.
            GestionnaireUI.gestUI.RetirerIndications();
            audioSource.Play();
            if (gameObject.tag != "Note")
            {
               Destroy(gameObject, 0.3f);
            }

            pickUp.SetPickupAlllowed(false);
        }

    }
}
