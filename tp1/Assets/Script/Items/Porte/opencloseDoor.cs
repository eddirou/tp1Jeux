using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
    public class opencloseDoor : MonoBehaviour
    {

        public Animator openandclose;
        public bool open;
        public bool estAccessible;
        public PickUpAllow pickUp;
        [SerializeField]
        private bool isLock;
        [SerializeField]
        private AudioSource porteOuvert;
        [SerializeField]
        private AudioSource porteBarrée;
        [SerializeField]
        private AudioSource porteFerme;

        void Start()
        {
            open = false;

            estAccessible = false;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Player")
            {
                estAccessible = true;
                GestionnaireUI.gestUI.AfficherIndications("Appueyr sur 'e' pour ouvrir ou fermer la porte");
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
                if (open == false && !isLock)
                {
                    StartCoroutine(opening());
                }
                if (open == true && !isLock)
                {
                    StartCoroutine(closing());
                }
                if (isLock)
                {
                    StartCoroutine(locked());
                }

            }
        }


        IEnumerator opening()
        {
            porteOuvert.Play();
            yield return new WaitForSeconds(0.6f);
            
            print("you are opening the door");
            openandclose.Play("Opening");
            open = true;

        }

        IEnumerator closing()
        {
            porteFerme.Play();
            yield return new WaitForSeconds(0.6f);
            
            print("you are closing the door");
            openandclose.Play("Closing");
            open = false;

        }

        IEnumerator locked()
        {
            porteBarrée.Play();
            GestionnaireUI.gestUI.RetirerIndications();
            GestionnaireUI.gestUI.AfficherIndications("cette porte est barrée");
            yield return new WaitForSeconds(0);
        }

        public void unlock()
        {
            isLock = false;
        }

    }
}