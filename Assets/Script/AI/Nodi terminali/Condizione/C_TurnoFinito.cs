using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TurnoFinito : Nodo
{
    public C_TurnoFinito(string name) : base(name)
    {
        this.name = name; 
    }

    public override Status Process()
    {
        if (GameManager.current.shiftIsOver)
        {
            return Status.Failure;
        }
        else
        {
            return Status.Success;
        }
    }
}
