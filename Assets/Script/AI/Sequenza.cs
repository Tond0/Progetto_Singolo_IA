using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Sequenza : Nodo
{
    public Sequenza() : base() 
    {
        name = "Sequence";
    }

    public override Status Process()
    {
        if (children[indexChild].Process() == Status.Running) return Status.Running;


        if (indexChild == children.Count - 1)
        {
            indexChild = 0;
            return Status.Success;
        }
        else
        {
            indexChild++;
            return Status.Running;
        }
    }
}
