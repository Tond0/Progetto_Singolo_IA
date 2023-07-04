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

        C_OrdineNonConcluso c_OrdineNonConcluso = new(dipendenteOnInteractable);

        //Se l'item che il dipendente deve prendere dopo non gli può essere fornito da questa area o ha finito l'ordine...
        if (dipendenteOnInteractable.nextInteractableType != type)
        {
            dipendenteOnInteractable = null; //Allora rende libera la postazione
            obstacle.enabled = true;
        }
    }

    //Azione1 = Rifornisci scorta.
    protected override void Azione1()
    {
        //dipendenteOnInteractable.carryingItem.Remove();

        quantitaItem = quantitaItemMassima;
    }
}
