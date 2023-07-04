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
        List<Item> ordineAppoggio = new(dipendente.cliente.order);

        foreach(Item itemPreparato in dipendente.carryingItem)
        {
            foreach(Item itemOrdinato in ordineAppoggio)
            {
                if (itemPreparato == itemOrdinato) { ordineAppoggio.Remove(itemOrdinato); break; }
            }
        }

        if (ordineAppoggio.Count > 0)
        {
            Debug.LogError(ordineAppoggio[0].ToString());
            //Facciamo capire al dipendente dove deve andare.
            dipendente.nextInteractableType = GameManager.current.ItemToInteractableType(ordineAppoggio[0]);

            return Status.Success;
        }
        else
        {
            dipendente.nextInteractableType = InteractableType.Cliente;
            return Status.Failure;
        }
    }
}
