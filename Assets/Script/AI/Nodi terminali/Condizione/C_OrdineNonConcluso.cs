using System.Collections;
using System.Collections.Generic;
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
        if (dipendente.carryingItem.Count != dipendente.cliente.order.Count)
        {
            //Facciamo capire al dipendente dove deve andare.
            dipendente.nextInteractableType = ItemToInteractableType(dipendente.cliente.order[dipendente.carryingItem.Count]);

            return Status.Success;
        }
        else
            return Status.Failure;
    }

    private InteractableType ItemToInteractableType(Item itemRichiesto)
    {
        switch (itemRichiesto)
        {
            case Item.PopCorn:
                return InteractableType.TecaPopCorn;

            case Item.Bibita:
                return InteractableType.Spina;

            case Item.Patatine:
                return InteractableType.TecaPatatine;
        }

        Debug.LogError("PROBLEMA");
        return InteractableType.Cliente;
    }
}
