using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ClientiDisponibili : Nodo
{
    private Dipendente dipendente;
    public C_ClientiDisponibili(Dipendente dipendente)
    {
        this.dipendente= dipendente;
    }

    public override Status Process()
    {
        Interactable cliente = GameManager.current.GetFreeInteractable(InteractableType.Cliente, dipendente);
        
        if(cliente != null)
            return Status.Success;
        else
            return Status.Failure;
    }
}
