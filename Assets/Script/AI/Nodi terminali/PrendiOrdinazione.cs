using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrendiOrdinazione : Nodo
{
    private readonly Dipendente dipendente;
    public PrendiOrdinazione(Dipendente dipendente)
    {
        this.dipendente = dipendente;
    }

    public override Status Process()
    {
        //Ascolta l'esigenze del cliente.
        dipendente.targetInteractable.Interact();

        if (dipendente.nextInteractable == InteractableType.Cliente)
            return Status.Running;
        else
        {
            return Status.Success;
        }
    }    
}
