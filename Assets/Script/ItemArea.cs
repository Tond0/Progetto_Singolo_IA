using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArea : Interactable
{
    //Azione0 = Prendi item
    protected override Status Azione0()
    {
        base.Azione0();

        if (quantitaItem <= 0) return Status.Failure;

        //Scala item
        if (type != InteractableType.Scorta) quantitaItem--;
        else dipendenteOnInteractable.staRifornendo = true;

        //AGGIUNGI L'ITEM!
        for(int i = 0; i < quantitaDrop; i++)
            dipendenteOnInteractable.carryingItem.Add(givenItem);


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
        base.Azione1();
        
        //Rimozione della scorta
        foreach(Item i in dipendenteOnInteractable.carryingItem)
        {
            if (i == Item.Scorta) { dipendenteOnInteractable.carryingItem.Remove(i); break; }
        }

        dipendenteOnInteractable.staRifornendo = false;

        quantitaItem = quantitaItemMassima;

        return Status.Success;
    }
}
