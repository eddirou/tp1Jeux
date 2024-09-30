using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptsNote : MonoBehaviour
{   
    private GameObject joueur;
    [SerializeField]
    private string messageNotes;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        joueur = GameObject.FindGameObjectWithTag("Player");
    }


    public void AfficherNote() 
    {
        GestionnaireUI.gestUI.AfficherNotes(messageNotes);
    }

    public void DropPages()
    {
        GestionnaireUI.gestUI.RetirerNotes();
    }
}
