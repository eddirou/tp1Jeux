using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Donne la sante des ennemis, peut �tre changer.
public class SanteEnnemi : MonoBehaviour
{
    [SerializeField]
    private int sante = 100;

    public void Blesser(int dommage)
    {
        sante -= dommage;

        if (sante <= 0)
        {
            Mourir();
        }
    }

    public void Mourir()
    {
        // � activer plus tard
        // navMeshAgent.isStopped = true;
        GetComponent<Animator>().SetTrigger("Mort");
        Invoke("Detruire", 5);
    }

    public void Detruire()
    {
        Destroy(gameObject);
    }


}