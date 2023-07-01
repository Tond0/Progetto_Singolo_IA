using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArea : Interactable
{
    //Azione0 = Prendi item
    protected override void Azione0()
    {
        InteractableType itemRichiesto = (InteractableType)Random.Range(0, 2);
        dipendenteOnInteractable.nextInteractable = itemRichiesto;
    }
}
