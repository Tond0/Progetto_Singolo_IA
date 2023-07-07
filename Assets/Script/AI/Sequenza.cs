using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Sequenza : Nodo
{
    public Sequenza(string name) : base(name) 
    {
    }

    public override Status Process()
    {
        if(base.Process() == Status.Failure) return Status.Failure;

        Debug.Log("Running " + name + " on: " + children[indexChild]);

        switch (children[indexChild].Process())
        {
            case Status.Running:
                return Status.Running;

            case Status.Failure:
                indexChild = 0;
                Debug.LogError("SEQUENZA RESTITUISCE FAILURE: " + name);
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
        return Status.Failure;
    }
}
