using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
