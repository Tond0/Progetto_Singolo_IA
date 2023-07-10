using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class Cliente : Interactable
{
    //TODO: Fix It
    public List<Item> order;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ordineTxt;

    private void Awake()
    {
        //Assegniamo il tipo di interactable Type
        Type = InteractableType.Cliente;

        //Assegniamo le interazioni possibili
        interazioniPossibili.Add(AzionePrimaria);
        interazioniPossibili.Add(AzioneSecondaria);
    }

    private void OnEnable()
    {
        order = SpawnOrder();
        ordineTxt.text = "";

        for (int i = 0; i < order.Count; i++)
        {
            switch (order[i])
            {
                case Item.Bibita:
                    ordineTxt.text += "Blu";
                    break;

                case Item.Patatine:
                    ordineTxt.text += "Azzurro";
                    break;

                case Item.PopCorn:
                    ordineTxt.text += "Viola";
                    break;
            }

            if (i < order.Count - 1)
            {
                ordineTxt.text += " + ";
            }
        }
    }

    private void OnDisable()
    {
        order.Clear();
        obstacle.enabled = true;
        dipendenteOnInteractable = null;
    }

    #region Spawn dell'ordine
    private List<Item> SpawnOrder()
    {
        List<Item> randomOrder = new List<Item>();

        int quantita = Random.Range(GameManager.current.grandezzaOrdineMinima, GameManager.current.grandezzaOrdineMassima + 1);

        for(int i = 0; i < quantita; i++)
        {
            randomOrder.Add(SpawnItem());
        }

        return randomOrder;
    }

    private Item SpawnItem()
    {
        Item randomItem = (Item)Random.Range(1,4);
        return randomItem;
    }
    #endregion
    
    //Prendi ordinazione
    public Status AzionePrimaria()
    {
        //L'interazione è finita quindi la resettiamo in vista di un'interazione futura.
        timerIsOver = false;
        //L'interazione è finita quindi lo comunichiamo al dipendente.
        dipendenteOnInteractable.InterazioneFinita = true;
        //Che cliente sta servendo il dipendente?
        dipendenteOnInteractable.cliente = this;


        return Status.Success;
    }

    //Consegna ordine
    public Status AzioneSecondaria()
    {
        //L'interazione è finita quindi la resettiamo in vista di un'interazione futura.
        timerIsOver = false;
        //L'interazione è finita quindi lo comunichiamo al dipendente.
        dipendenteOnInteractable.InterazioneFinita = true;
        //Il dipendente non sta più seguendo questo cliente
        dipendenteOnInteractable.cliente = null;


        //Togliamo gli item inerenti all'ordine
        for (int i = 0; i < dipendenteOnInteractable.carryingItem.Count; i++)
        {
            if (dipendenteOnInteractable.carryingItem[i] != Item.Scorta)
            {
                dipendenteOnInteractable.carryingItem.RemoveAt(i);
                i--; //Evitiamo che si aggiorni.
            }
        }

        //Spegniamo il cliente
        gameObject.SetActive(false);

        return Status.Success;
    }
}
