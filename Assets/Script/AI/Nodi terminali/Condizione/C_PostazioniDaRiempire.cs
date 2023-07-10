using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class C_PostazioniDaRiempire : Nodo
{
    readonly Dipendente dipendente;
    public C_PostazioniDaRiempire(Dipendente dipendente)
    {
        this.dipendente = dipendente;
    }

    public override Status Process()
    {
        if(base.Process() == Status.Failure) return Status.Failure;

        AreaItem areaDaRifornire = GameManager.current.GetFreeAreaItemRifornibile(dipendente);
        if (areaDaRifornire)
        {
            areaDaRifornire.dipendenteOnInteractable = dipendente;
            dipendente.nextInteractableType = areaDaRifornire.Type;
            return Status.Success;
        }
        else
        {
            return Status.Failure;
        }
    }
}
