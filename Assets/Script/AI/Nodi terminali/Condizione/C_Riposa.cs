using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Riposa : Nodo
{
    Dipendente dipendente;
    public C_Riposa(Dipendente dipendente) 
    {
        this.dipendente = dipendente;
    }
    public override Status Process()
    {
        if(dipendente.StaRiposando)
            return Status.Success;
        else 
            return Status.Failure;
    }
}
