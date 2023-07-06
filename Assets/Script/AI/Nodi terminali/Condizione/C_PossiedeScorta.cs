using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_PossiedeScorta : Nodo
{
    readonly Dipendente dipendente;
    public C_PossiedeScorta(Dipendente dipendente) : base() 
    {
        this.dipendente= dipendente;
    }
    public override Status Process()
    {
        if(base.Process() == Status.Failure) return Status.Failure;

        foreach(Item i in dipendente.carryingItem)
        {
            if (i == Item.Scorta) return Status.Success;
        }

        return Status.Failure;
    }
}
