using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApprocciaCliente : Nodo
{
    public override Status Process()
    {
        Debug.Log("Approccia cliente");
        return Status.Success;
    }    
}
