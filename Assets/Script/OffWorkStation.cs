using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffWorkStation : Interactable
{
    private void Awake()
    {
        Type = InteractableType.StazioneDiRiposo;

        interazioniPossibili.Add(AzionePrimaria); 
    }

    //AzionePrimaria = dormire
    protected Status AzionePrimaria()
    {
        //L'interazione è finita quindi la resettiamo in vista di un'interazione futura.
        timerIsOver = false;
        //L'interazione è finita quindi lo comunichiamo al dipendente.
        dipendenteOnInteractable.InterazioneFinita = true;
        //Il dipendente è sleepy sleepy
        dipendenteOnInteractable.StaRiposando = true;

        return Status.Success;
    }
}
