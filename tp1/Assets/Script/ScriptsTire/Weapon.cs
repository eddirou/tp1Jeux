using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Le gun
// Maxime Nobert
public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpCamera;
    [SerializeField] float distance = 100f;
    [SerializeField] int dommage = 20;
    [SerializeField] int munitions = 15;
    [SerializeField] int chargeur = 45;
    private int munitionsMax = 15; // Munitions maximales dans le chargeur
    [SerializeField] private TMP_Text munitionsText;
    [SerializeField] private ParticleSystem muzzleFlash;
    private const int MultiplicateurDommageTete = 5;

    [SerializeField] private AudioSource audioSourceTir; // AudioSource pour le son de tir
    [SerializeField] private AudioSource audioSourceRecharge;

    private Vector3 rotationInitiale;

    private bool peutTirer = true;
    private bool peutRecharger = true; // Indique si le joueur peut recharger

    [SerializeField]private float fireRate = 0.4f;

    private float dernierTir;

    private void Start()
    {
        rotationInitiale = transform.localEulerAngles;
        MettreAJourUI(); // Met à jour l'UI au début
        dernierTir = -fireRate;
    }

    public void Tirer()
    {
        if (!peutTirer || munitions <= 0) // Vérifie s'il reste des munitions
        {
            Debug.Log("Plus de munitions !");
            return; // Ne tire pas si pas de munitions
        }

        if (this.gameObject.activeSelf == false)
        {
            return;
        }

        // Tire si le firerate est respecté
        if(Time.timeSinceLevelLoad - dernierTir >= fireRate)
        {  
            transform.localEulerAngles = rotationInitiale;

            RaycastHit hit;

            JouerSonTir();

            AfficherEffetTir();

            if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, distance))
            {
                Debug.Log("J'ai tiré sur cet objet : " + hit.transform.name);
    
                // TODO: CHANGER LES TAGS POURS CEUX DES ENNEMIS
                if (hit.transform.tag == "Ennemi" || hit.transform.tag == "EnnemiTete")
                {
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

            }
            dernierTir = Time.timeSinceLevelLoad;

        munitions--;
        MettreAJourUI();
        }
    }

    private void JouerSonTir()
    {
        audioSourceTir.Play();
    }

    private void JouerSonReload()
    {
        audioSourceRecharge.Play();
    }

    public void Recharger()
    {
        if (!peutRecharger || munitions == munitionsMax) return;
        StartCoroutine(RechargerCoroutine());
    }

    public int getChargeur()
    {
        return chargeur;
    }

    public void setChargeur(int munitionsAjouté)
    {
        chargeur += munitionsAjouté;
    }

    private IEnumerator RechargerCoroutine()
    {
        peutTirer = false; // Empêche le tir pendant le rechargement
        Debug.Log("Rechargement...");
        JouerSonReload();

        transform.Rotate(-25, 0, 0);

        yield return new WaitForSeconds(2); // Délai de 2 secondes

        transform.Rotate(25, 0, 0);


        // Conditions de rechargement
        if (munitions < 15) // Si les munitions dans le chargeur sont inférieures à 15
        {
            int munitionsManquantes = munitionsMax - munitions; // Munitions nécessaires pour remplir le chargeur

            if (chargeur >= munitionsManquantes) // Si le chargeur a suffisamment de munitions
            {
                munitions += munitionsManquantes;
                chargeur -= munitionsManquantes;
            }
            else
            {
                munitions += chargeur;
                chargeur = 0;
            }
        }

        // Vérifier si le chargeur est vide
        if (chargeur <= 0)
        {
            chargeur = 0;
            peutRecharger = false;
            peutTirer = false;
            Debug.Log("Plus de munitions dans le chargeur !");
        }

        MettreAJourUI(); // Met à jour l'UI après le rechargement
        
        peutTirer = true; // Permet de tirer à nouveau
    }
    private void AfficherEffetTir()
    {
        muzzleFlash.Play();
    }

    private void MettreAJourUI()
    {
        if (munitionsText != null)
        {
            munitionsText.text = "Chargeur: " + munitions + " / " + munitionsMax + "\nMunitions : " + chargeur; // Met à jour le texte avec le nombre de munitions
        }
    }

}
