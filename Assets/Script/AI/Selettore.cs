using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Selettore : Nodo
{
    public Selettore(string nome) : base(nome) 
    {
        
    }

    public override Status Process()
    {
        if (base.Process() == Status.Failure) return Status.Failure;

        //Debug.Log("Running " + name + " on: " + children[indexChild].name);
        //Debug.Log("");

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
