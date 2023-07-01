using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Debug.Log(name);
        switch (children[indexChild].Process())
        {
            case Status.Running:
                return Status.Running;
            

            case Status.Failure:
                indexChild = 0;
                return Status.Failure;


            case Status.Success:
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
        Debug.LogError("Something went wrong");
        return Status.Failure;
    }
}
