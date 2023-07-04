using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class C_OrdineNonConcluso : Nodo
{
    readonly Dipendente dipendente;
    public C_OrdineNonConcluso(Dipendente dipendente)
    {
        this.dipendente = dipendente;
    }

    public override Status Process()
    {
        Item nextItem = dipendente.NextItemInOrder();

        if (nextItem == Item.Niente) return Status.Failure;

        Debug.LogError(nextItem.ToString());

        //Facciamo capire al dipendente dove deve andare.
        dipendente.nextInteractableType = GameManager.current.ItemToInteractableType(nextItem);

        return Status.Success;

    }
}
