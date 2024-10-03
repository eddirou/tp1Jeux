using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject arme1;
    [SerializeField] GameObject arme2;
    [SerializeField] GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        arme1 = GameObject.FindGameObjectWithTag("weapon1");
        arme2 = GameObject.FindGameObjectWithTag("melee");

        arme2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPrimaire()
    {
        arme1.SetActive(true);
        arme2.SetActive(false);
        UI.SetActive(true);
    }

    public void SwitchSecondaire()
    {
        arme1.SetActive(false);
        arme2.SetActive(true);
        UI.SetActive(false);
    }
}
