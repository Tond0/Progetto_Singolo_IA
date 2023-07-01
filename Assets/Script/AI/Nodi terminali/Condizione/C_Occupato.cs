using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Occupato : Nodo
{
    private Dipendente dipendente;
    public C_Occupato(Dipendente dipendente)
    {
        this.dipendente= dipendente;
    }

    public override Status Process()
    {
        Interactable cliente = GameManager.current.GetFreeInteractable(dipendente.agent, InteractableType.Cliente);
        
        if(cliente == null)
            return Status.Success;
        else
            return Status.Failure;
    }
}
