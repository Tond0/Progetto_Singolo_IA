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
        if (GameManager.current.ShiftIsOver)
        {
            return Status.Failure;
        }
        else
        {
            return Status.Success;
        }
    }
}
