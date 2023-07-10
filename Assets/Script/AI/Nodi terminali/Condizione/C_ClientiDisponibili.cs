using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ClientiDisponibili : Nodo
{
    private readonly Dipendente dipendente;
    public C_ClientiDisponibili(Dipendente dipendente)
    {
        this.dipendente= dipendente;
    }

    public override Status Process()
    {
        List<Interactable> clientiDisponibili = GameManager.current.GetFreeInteractables(InteractableType.Cliente, dipendente);

        if (clientiDisponibili.Count > 0)
        {
            return Status.Success;
        }
        else
            return Status.Failure;
    }
}
