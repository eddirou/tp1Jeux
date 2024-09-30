using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAllow : MonoBehaviour
{
 public bool pickupAllowed = false;

    public bool notesEnMain = false;

    public void Start()
    {
        pickupAllowed = false ;
    }


    public void SetPickupAlllowed(bool value)
    {
        if (value)
        {
            Invoke("DeactivatePickup", 1);
        }

        pickupAllowed = value;
    }

    public bool PickupAllowed()
    {
        return pickupAllowed;
    }


    private void DeactivatePickup()
    {
        pickupAllowed = false;
    }



    public void PickUpNote()
    {
        notesEnMain = true;
    }
    public void dropNote()
    {
        notesEnMain = false;
    }
}
