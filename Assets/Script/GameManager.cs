using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{
    #region instanza
    public static GameManager current;
    private void Awake()
    {
        if (current != null) Destroy(this);
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
    [SerializeField] private bool shiftOffDebug;
    #endregion

    [Header("Game navigation")]
    public NavMeshSurface surface;
    public List<Interactable> oggettiInteragibili = new();
    public List<Dipendente> Dipendenti = new();


    private void OnValidate()
    {
        ShiftIsOver = !shiftOffDebug;
    }

    public bool ShiftIsOver { get; private set; }

    private void Start()
    {
        foreach (Interactable i in oggettiInteragibili)
        {
            if (i.Type != InteractableType.Scorta) i.gameObject.SetActive(false);
        }

        //Shift cycle setUp (funzionava ma per creare una scena di test pi� carina voglio far decidere al tester quando finire il turno)
        //StartCoroutine(ShiftCycle());
    }


    #region Trovare il percorso all'interactable pi� vicino
    public List<Interactable> GetFreeInteractables(InteractableType typeNeeded, Dipendente dipendente)
    {
        List<Interactable> interactablesLiberi = new();

        foreach (Interactable i in oggettiInteragibili)
        {
            if (!i.dipendenteOnInteractable && i.Type == typeNeeded && i.gameObject.activeSelf)
            {
                interactablesLiberi.Add(i);
            }

            if (i.dipendenteOnInteractable == dipendente && i.Type == typeNeeded && i.gameObject.activeSelf)
            {
                interactablesLiberi.Clear();
                interactablesLiberi.Add(i);
                i.obstacle.enabled = false;
                return interactablesLiberi;
            }
        }

        return interactablesLiberi;
    }

    public AreaItem GetFreeAreaItemRifornibile(Dipendente dipendente)
    {
        foreach (Interactable i in oggettiInteragibili)
        {
            //Se l'interactable � un'area item, che non sta venendo utilizzata, che non � maxxata...
            if (i.TryGetComponent(out AreaItem areaItem) && (!areaItem.dipendenteOnInteractable || areaItem.dipendenteOnInteractable == dipendente) && areaItem.quantitaItem < areaItem.quantitaItemMassima) return areaItem;
        }

        return null;
    }

    /* Cosa che ho provato a fare ma devo andare a lavorare ora quindi mi sa che non raggiunge il gioco finale T.T
    private Dictionary<List<Interactable>, AsyncOperation> UpdateNavMeshStatus = new Dictionary<List<Interactable>, AsyncOperation>();
    public Interactable FindNearest(List<Interactable> interactablesLiberi, NavMeshAgent agentDipendente)
    {

        foreach(Interactable i in interactablesLiberi)
            i.obstacle.enabled = false;

        Debug.LogError(UpdateNavMeshStatus.ContainsKey(interactablesLiberi));

        if (UpdateNavMeshStatus.ContainsKey(interactablesLiberi))
        {
            UpdateNavMeshStatus.TryGetValue(interactablesLiberi, out AsyncOperation operation);
            if (operation.isDone)
            {
                NavMeshPath path = new();
                float pathCost = Mathf.Infinity;
                Interactable nearestInteractable = null;

                foreach (Interactable i in interactablesLiberi)
                {
                    agentDipendente.CalculatePath(i.transform.position, path);
                    float pathCalcolato = CalculatePathCost(path);

                    if (pathCalcolato < pathCost)
                    {
                        nearestInteractable = i;
                        pathCost = pathCalcolato;
                    }
                }

                return nearestInteractable;
            }
            else
            {
                return null;
            }
            
        }
        else
        {
            Debug.LogError("QUA");
            UpdateNavMeshStatus.TryAdd(interactablesLiberi, surface.UpdateNavMesh(surface.navMeshData));
            return null;   
        }
    }

    private float CalculatePathCost(NavMeshPath path)
    {
        float cost = 0f;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            cost += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            Debug.LogError(cost);
        }

        return cost;
    }
    */
    #endregion

    #region Funzioni per traduzione tra enum
    public Item InteractableTypeToItem(InteractableType interactableType)
    {
        switch (interactableType)
        {
            case InteractableType.TecaPopCorn:
                return Item.PopCorn;

            case InteractableType.Spina:
                return Item.Bibita;

            case InteractableType.TecaPatatine:
                return Item.Patatine;

            case InteractableType.Scorta:
                return Item.Scorta;
        }

        Debug.LogError("Stai cercando un area che non da un item!");
        return Item.Niente;
    }

    public InteractableType ItemToInteractableType(Item item)
    {
        switch (item)
        {
            case Item.Bibita:
                return InteractableType.Spina;

            case Item.Patatine:
                return InteractableType.TecaPatatine;

            case Item.PopCorn:
                return InteractableType.TecaPopCorn;

            case Item.Scorta:
                return InteractableType.Scorta;
        }
        Debug.LogError("Stai cercando un item che non ha un area!");
        return InteractableType.Null;
    }

    #endregion

    #region Shift cycles
    IEnumerator ShiftCycle()
    {
        ShiftIsOver = true;
        while (true)
        {
            yield return new WaitForSeconds(durataTurno);
            ShiftIsOver = false;
            yield return new WaitForSeconds(durataPausa);
            ShiftIsOver = true;
        }
    }
    #endregion

    #region Buttons events
    //TODO: Actions.
    public void AddDipendente()
    {
        foreach (Dipendente d in Dipendenti)
        {
            if (!d.gameObject.activeSelf)
            {
                d.gameObject.SetActive(true);
                AddArea(InteractableType.StazioneDiRiposo);
                return;
            }
        }
    }

    public void SwitchShiftStatus()
    {
        Debug.LogWarning("Turno: " + ShiftIsOver);
        ShiftIsOver = !ShiftIsOver;
    }

    public void AddArea(InteractableType type)
    {
        foreach (Interactable i in oggettiInteragibili)
        {
            if (i.Type == type && !i.gameObject.activeSelf) { i.gameObject.SetActive(true); return; }
        }
    }


    #endregion
}
