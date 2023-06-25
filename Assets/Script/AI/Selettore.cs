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
        //Se il nodo sta ancora processando sta processando anche lui
        if (children[indexChild].Process() == Status.Running) return Status.Running;
        
        //Se il nodo è un successo allora esce.
        else if(children[indexChild].Process() == Status.Success) return Status.Success;
        
        //Se è arrivato all'ultimo suo nodo e non ha riscontrato successi...
        if (indexChild == children.Count - 1)
        {
            indexChild = 0;
            //Restituisce failure
            return Status.Failure;
        }
        //Se ha altri figli passa a quello successivo.
        else
        {
            indexChild++;
            return Status.Running;
        }
    }
}
