using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaItem : Nodo
{
    public override Status Process()
    {
        Debug.Log("PortaItem");
        return Status.Success;
    }
}
