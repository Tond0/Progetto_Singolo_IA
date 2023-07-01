using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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


    #region Game balancing
    
    [Header("Game balancing")]
    [Header("Cliente")]
    public int grandezzaOrdineMinima;
    public int grandezzaOrdineMassima;
    [Header("Shift Cycle")]
    [SerializeField] private float durataTurno;
    [SerializeField] private float durataPausa;

    [Header("Debug stuff, da non toccare")]
    [SerializeField] private bool shiftOff;
    [SerializeField] private List<Interactable> OggettiInteragibili = new();

    private void OnValidate()
    {
        shiftStatus = !shiftOff;
    }

    public bool shiftStatus { get; private set; }
     
    #endregion

    private void Start()
    {
        OggettiInteragibili = FindObjectsOfType<Interactable>().ToList();

        //Shift cycle setUp
        //StartCoroutine(ShiftCycle());
    }

    #region Trovare il percorso all'interactable più vicino
    public Interactable GetFreeInteractable(NavMeshAgent dipendente, InteractableType typeNeeded)
    {
        List<Interactable> interactablesLiberi = new();

        foreach(Interactable i in OggettiInteragibili)
        {
            if(!i.dipendenteOnInteractable && i.type == typeNeeded) interactablesLiberi.Add(i);
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

    #region Shift cycles
    IEnumerator ShiftCycle()
    {
        shiftStatus = true;
        while (true)
        {
            yield return new WaitForSeconds(durataTurno);
            shiftStatus = false;   
            yield return new WaitForSeconds(durataPausa);
            shiftStatus = true;   
        }
    }
    #endregion
}
