using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Compilation;
using UnityEngine;

public enum Status { Success, Running, Failure };

public abstract class Nodo
{
    public Status state { get; protected set; }

    public List<Nodo> children = new List<Nodo>();

    public List<Nodo> exitConditions = new();

    public int indexChild;

    protected int indexCondition;

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

    public void AddExitCondition(Nodo exitCondition)
    {
        exitConditions.Add(exitCondition);
    }

    public virtual Status Process() 
    {
        foreach (Nodo c in exitConditions) 
        {
            if (c.Process() == Status.Failure) return Status.Failure;
        }

        return Status.Success;
    }

    public void PrintChildren(Nodo nodo)
    {
        Debug.Log(nodo.name);
        foreach(Nodo nChild in nodo.children)
        {
            nChild.PrintChildren(nChild);
        }
    }
}
