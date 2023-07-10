using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorta : Interactable
{
    [Header("Scorta settings")]
    [SerializeField] protected int quantitaDrop = 1;
    private Item givenItem;

    private void Start()
    {
        //Assegnazione tipologia
        Type = InteractableType.Scorta;

        //Assegnazione item.
        givenItem = GameManager.current.InteractableTypeToItem(Type);

        //Assengazione azioni possibili
        interazioniPossibili.Add(AzionePrimaria);
    }

    //AzionePrimaria = prendere item.
    public Status AzionePrimaria()
    {
        //L'interazione è finita quindi la resettiamo in vista di un'interazione futura.
        timerIsOver = false;
        //L'interazione è finita quindi lo comunichiamo al dipendente.
        dipendenteOnInteractable.InterazioneFinita = true;
        //Il dipendente si sta rifornendo.
        dipendenteOnInteractable.StaRifornendo = true;


        //Aggiungi gli item
        for (int i = 0; i < quantitaDrop; i++)
        {
            dipendenteOnInteractable.carryingItem.Add(givenItem);
        }


        dipendenteOnInteractable = null; //Allora rende libera la postazione
        obstacle.enabled = true; //E la resetta come ostacolo sennò sono stupidi e si incastrano

        return Status.Success;
    }
}
