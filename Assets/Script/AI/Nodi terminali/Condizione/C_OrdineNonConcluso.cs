using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class C_OrdineNonConcluso : Nodo
{
    readonly Dipendente dipendente;
    public C_OrdineNonConcluso(Dipendente dipendente, string name) : base(name)
    {
        this.dipendente = dipendente;
        this.name = name;
    }

    public override Status Process()
    {

        Item nextItem = dipendente.NextItemInOrder();

        //Se non ha altri item in ordine allora ha finito l'ordine!
        if (nextItem == Item.Niente)
        {
            dipendente.nextInteractableType = InteractableType.Cliente;
            dipendente.targetInteractable = dipendente.cliente;
            return Status.Failure;
        }


        //Facciamo capire al dipendente dove deve andare.
        dipendente.nextInteractableType = GameManager.current.ItemToInteractableType(nextItem);

        return Status.Success;

    }
}
