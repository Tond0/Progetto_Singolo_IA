using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_NonStaGiaRifornendo : Nodo
{
    readonly Dipendente dipendente;
    public C_NonStaGiaRifornendo(Dipendente dipendente) 
    {
        this.dipendente = dipendente;
    }

    public override Status Process()
    {
        if(base.Process() == Status.Failure) return Status.Failure;

        if(dipendente.StaRifornendo)
            return Status.Failure;
        else 
            return Status.Success;
    }
}
