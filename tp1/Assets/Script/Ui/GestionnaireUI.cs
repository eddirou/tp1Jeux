using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GestionnaireUI : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI textPopUp;
    [SerializeField]
    private TextMeshProUGUI textNote;

    public static GestionnaireUI gestUI { get; private set; }

    void Start()
    {

    }

    public void Awake()
    {
        gestUI = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AfficherIndications(string indications, int? delaiAffichage = null)
    {
        textPopUp.text = indications;

        if (delaiAffichage != null)
        {
            Invoke("RetirerIndications", (float)delaiAffichage);
        }
    }

    public void RetirerIndications()
    {
        textPopUp.text = string.Empty;
    }


    public void AfficherNotes(string notes)
    {
        textNote.text = notes;
    }

    public void RetirerNotes()
    {
        textNote.text = string.Empty;
    }
}
