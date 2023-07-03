using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArea : Interactable
{
    //Azione0 = Prendi item
    protected override void Azione0()
    {
        //AGGIUNGI L'ITEM!
        dipendenteOnInteractable.carryingItem.Add(givenItem);

        base.Azione0();

        if (dipendenteOnInteractable.cliente.order.Count == dipendenteOnInteractable.carryingItem.Count
                || dipendenteOnInteractable.cliente.order[dipendenteOnInteractable.carryingItem.Count] != givenItem)
            dipendenteOnInteractable = null;
    }
}
