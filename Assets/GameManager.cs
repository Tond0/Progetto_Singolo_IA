using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    #region instanza
    public static GameManager current;
    private void Awake()
    {
        if(current != null) Destroy(this);
        else current = this;
    }
    #endregion

    [SerializeField] private List<Interactable> OggettiInteragibili = new();

    #region Trovare il percorso all'interactable più vicino
    public Interactable GetFreeInteractable(NavMeshAgent dipendente, InteractableType typeNeeded)
    {
        List<Interactable> interactablesLiberi = new();

        foreach(Interactable i in OggettiInteragibili)
        {
            if(!i.occupied && i.type == typeNeeded) interactablesLiberi.Add(i);
            Debug.Log("Loading... " + (i.type == typeNeeded));
        }

        if(interactablesLiberi.Count <= 0) return null;
        
        NavMeshPath path = new();
        float pathCost = Mathf.Infinity;
        Interactable nearestInteractable = null;
        foreach(Interactable i in interactablesLiberi)
        {
            dipendente.CalculatePath(i.transform.position, path);
            float pathCalcolato = CalculatePathCost(path);

            if (pathCalcolato < pathCost)
            {
                nearestInteractable = i;
                pathCost = pathCalcolato;
            }
        }

        nearestInteractable.occupied = true;

        return nearestInteractable;
    }

    private float CalculatePathCost(NavMeshPath path)
    {
        float cost = 0f;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            cost += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return cost;
    }
    #endregion
}
