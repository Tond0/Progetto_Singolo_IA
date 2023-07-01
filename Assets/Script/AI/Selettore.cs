using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selettore : Nodo
{
    public Selettore() : base() 
    {
        name = "Selection";
    }

    public override Status Process()
    {
        switch (children[indexChild].Process())
        {
            case Status.Running:
                return Status.Running;


            case Status.Failure:
                if (indexChild == children.Count - 1)
                {
                    indexChild = 0;
                    return Status.Failure;
                }
                else
                {
                    indexChild++;
                    return Status.Running;
                }


            case Status.Success:
                indexChild = 0;
                return Status.Success;
        }
        Debug.LogError("Something went wrong");
        return Status.Failure;
    }
}
