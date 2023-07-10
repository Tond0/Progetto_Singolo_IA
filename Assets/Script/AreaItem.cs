using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaItem : Interactable
{
    [Header("Area settings")]
    [SerializeField] private InteractableType tipoAreaItem;
    public int quantitaItemMassima;
    public int quantitaItem;
    [SerializeField] private TextMeshProUGUI quantityTxt;
    [SerializeField] protected int quantitaDrop = 1;
    private Item givenItem;

    private void Awake()
    {
        Type = tipoAreaItem;
    }

    private void Start()
    {
        //Fulliamo allo start.
        quantitaItem = quantitaItemMassima;

        quantityTxt.text = quantitaItem + " / " + quantitaItemMassima;

        //Assegnazione item.
        givenItem = GameManager.current.InteractableTypeToItem(Type);

        //Assengazione azioni possibili
        interazioniPossibili.Add(AzionePrimaria);
        interazioniPossibili.Add(AzioneSecondaria);
    }

    //AzionePrimaria = prendere item.
    public Status AzionePrimaria()
    {
        //L'interazione � finita quindi la resettiamo in vista di un'interazione futura.
        timerIsOver = false;
        //L'interazione � finita quindi lo comunichiamo al dipendente.
        dipendenteOnInteractable.InterazioneFinita = true;


        //Se sono finiti gli item allora ritorna failure e il dipendente andr� a cercare una scorta.
        if (quantitaItem <= 0) return Status.Failure;
        //dipendenteOnInteractable.StaRifornendo = true;



        //Aggiungi gli item
        for (int i = 0; i < quantitaDrop; i++)
        {
            dipendenteOnInteractable.carryingItem.Add(givenItem);
            quantitaItem--;
            quantityTxt.text = quantitaItem + " / " + quantitaItemMassima;
        }


        //Se, controllando l'ordine che deve svolgere, dopo gli serve lo stesso tipo di item non rende libera la stazione e la usa ancora.
        if (NeededAgain())
        {
            dipendenteOnInteractable = null; //Allora rende libera la postazione
            obstacle.enabled = true; //E la resetta come ostacolo senn� sono stupidi e si incastrano
        }

        return Status.Success;

        /*if (Type == InteractableType.Scorta)
        {
            dipendenteOnInteractable = null; //Allora rende libera la postazione
            obstacle.enabled = true;
        }
        */
    }

    //AzioneSecondaria = rifornire l'area item.
    public Status AzioneSecondaria()
    {
        //L'interazione � finita quindi la resettiamo in vista di un'interazione futura.
        timerIsOver = false;
        //L'interazione � finita quindi lo comunichiamo al dipendente.
        dipendenteOnInteractable.InterazioneFinita = true;
        //Il dipendente non sta pi� rifornendo
        dipendenteOnInteractable.StaRifornendo = false;


        //Rimozione della scorta
        foreach (Item i in dipendenteOnInteractable.carryingItem)
        {
            if (i == Item.Scorta) { dipendenteOnInteractable.carryingItem.Remove(i); break; }
        }


        //Restock
        quantitaItem = quantitaItemMassima;
        quantityTxt.text = quantitaItem + " / " + quantitaItemMassima;



        //Se sta riempiendo la postazione perch� gli servir� in futuro allora la teniamo locckata a lui, se invece sta riempiendo le postazioni perch� non ha niente di meglio da fare allora la rende subito libera.
        if (NeededAgain())
        {
            dipendenteOnInteractable = null; //Allora rende libera la postazione
            obstacle.enabled = true; //E la rimette come ostacolo senn� si incastrano tutti
        }

        return Status.Success;
    }

    private bool NeededAgain()
    {
        //Otteniamo il prossimo item che il dipendente deve prendere.
        Item nextItem = dipendenteOnInteractable.NextItemInOrder();

        //Lo convertiamo in un interactableType
        InteractableType nextItemType = GameManager.current.ItemToInteractableType(nextItem);

        return nextItemType != Type;
    }

}
