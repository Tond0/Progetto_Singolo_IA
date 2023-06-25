using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condizione : Nodo
{
    public bool condizione { get; private set; }

    public Condizione(string n, bool condizione) : base(n)
    {
        name = n;
        this.condizione = condizione;
    }

    public override Status Process()
    {
        if(condizione)
            return Status.Success;
        else
            return Status.Failure;
    }
}
