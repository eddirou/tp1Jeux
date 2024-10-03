using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] Camera fpCamera;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int dommage = 40;

    [SerializeField] private float range = 7f;

    [SerializeField] private AudioSource audioSourceHit;
    [SerializeField] private AudioSource audioSourceRate;

    private const int MultiplicateurDommageTete = 2;
    private float derniereFrappe;

    private Vector3 rotationSwing;
    private Vector3 rotationInitiale;

    // Start is called before the first frame update
    void Start()
    {
        rotationInitiale = transform.localEulerAngles;
        rotationSwing = new Vector3(60, 0, 0);
        derniereFrappe = -fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Attaque avec la batte, activer avec clic droit
    public void Attaque()
    {
        if(this.gameObject.activeSelf)
        {
            
            if (Time.timeSinceLevelLoad - derniereFrappe >= fireRate)
            {
                StartCoroutine(AnimeAttaque());
                RaycastHit hit;
                if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, range))
                {
                    
                    Debug.Log("J'ai frappé sur cet objet : " + hit.transform.name);

                    // TODO: CHANGER LES TAGS POURS CEUX DES ENNEMIS
                    if (hit.transform.tag == "Ennemi" || hit.transform.tag == "EnnemiTete")
                    {
                        JouerSonHit();

                        // TODO: UTILISER WHATEVER QUI SERT POUR LA VIE DES ENNEMIS
                        SanteEnnemi victime;
                        int dommageEnnemi = dommage;

                        if (hit.transform.tag == "Ennemi")
                        {
                            victime = hit.transform.GetComponent<SanteEnnemi>();
                        }
                        else
                        {
                            victime = hit.transform.parent.GetComponent<SanteEnnemi>();
                            dommageEnnemi = dommage * MultiplicateurDommageTete;
                        }

                        if (victime != null)
                            victime.Blesser(dommageEnnemi);
                    }
                } else
                {
                    JouerSonRate();
                }

                
                derniereFrappe = Time.timeSinceLevelLoad;
            }
        }
    }

    // Fait l'animation du swing
    private IEnumerator AnimeAttaque()
    {
        float temps = 0f;

        // Rotation de l'objet vers la rotation cible
        while (temps < 0.5f)
        {
            transform.localEulerAngles = Vector3.Lerp(rotationInitiale, rotationSwing, temps / 0.5F);
            temps += Time.deltaTime;
            yield return null;
        }

        // Retourner à la rotation initiale
        transform.localEulerAngles = rotationInitiale;
    }

    // Si on touche un ennemi
    private void JouerSonHit()
    {
        audioSourceHit.Play();
    }

    // Si on swing dans le vide
    private void JouerSonRate()
    {
        audioSourceRate.Play();
    }
}
