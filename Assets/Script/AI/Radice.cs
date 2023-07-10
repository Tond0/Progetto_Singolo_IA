using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radice : Nodo
{
    public Radice()
    {
        name = "Root";
    }

    public override Status Process()
    {
        return children[indexChild].Process();
    }

    struct LivelloNodo
    {
        public Nodo node;
        public int level;
    }
}
