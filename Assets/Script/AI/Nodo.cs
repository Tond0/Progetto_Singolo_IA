using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public enum Status { Success, Running, Failure };

public abstract class Nodo
{
    public Status state { get; protected set; }

    public List<Nodo> children = new List<Nodo>();

    protected int indexChild;

    public string name;

    public Nodo() { }

    public Nodo(string n)
    {
        name = n;
    }

    public void AddChild(Nodo n)
    {
        children.Add(n);
    }

    public abstract Status Process();

    public void PrintChildren(Nodo nodo)
    {
        Debug.Log(nodo.name);
        foreach(Nodo nChild in nodo.children)
        {
            nChild.PrintChildren(nChild);
        }
    }
}
