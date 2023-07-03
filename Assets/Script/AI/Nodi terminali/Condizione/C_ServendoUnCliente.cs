using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ServendoUnCliente : Nodo
{
    Dipendente dipendente;
    public C_ServendoUnCliente(Dipendente dipendente)
    {
        this.dipendente = dipendente;
    }
    public override Status Process()
    {
        if (dipendente.cliente != null)
            return Status.Success;
        else
            return Status.Failure;
    }
}
