using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArea : Interactable
{
    //Azione0 = Prendi item
    protected override Status Azione0()
    {
        if (quantitaItem <= 0) return Status.Failure;

        //AGGIUNGI L'ITEM!
        dipendenteOnInteractable.carryingItem.Add(givenItem);

        base.Azione0();

        Item nextItem = dipendenteOnInteractable.NextItemInOrder();
        
        InteractableType nextItemType = GameManager.current.ItemToInteractableType(nextItem);

        //Se l'item che il dipendente deve prendere dopo non gli può essere fornito da questa area o ha finito l'ordine...
        if (nextItemType != type)
        {
            dipendenteOnInteractable = null; //Allora rende libera la postazione
            obstacle.enabled = true;
        }

        return Status.Success;
    }

    //Azione1 = Rifornisci scorta.
    protected override Status Azione1()
    {
        //dipendenteOnInteractable.carryingItem.Remove();

        quantitaItem = quantitaItemMassima;

        return Status.Success;
    }
}
