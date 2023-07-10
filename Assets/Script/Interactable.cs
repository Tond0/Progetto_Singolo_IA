using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum InteractableType { TecaPopCorn, Spina, TecaPatatine, [InspectorName(null)] Cliente, [InspectorName(null)] StazioneDiRiposo, [InspectorName(null)] Scorta, [InspectorName(null)] Null }
public class Interactable : MonoBehaviour
{
    [Header("Interactable stuff")]
    public float durataInterazione;
    public NavMeshObstacle obstacle;
    [Header("Per debug, da non modificare")]
    public InteractableType Type; //Metto la possibilità di scegliere anche se in molti script poi forzo il suo Type per sicurezza.
    public Dipendente dipendenteOnInteractable;

    protected delegate Status Azione();
    protected List<Azione> interazioniPossibili = new();
    
    #region Interact Manager
    public Status Interact(int azione)
    {
        //Finito il tempo eseguiamo l'azione richiesta.
        if (timerIsOver)
        {
            return interazioniPossibili[azione]();
        }

        //Facciamo passare il tempo d'interazione richiesto e facciamo muovere lo slider.
        if (waitCoroutine == null && !timerIsOver)
            waitCoroutine = StartCoroutine(WaitTime());


        return Status.Running;
    }
    #endregion

    #region Timer
    private float timer = 0;
    protected bool timerIsOver = false;
    protected Coroutine waitCoroutine;
    protected IEnumerator WaitTime()
    {
        //SetUp slider
        dipendenteOnInteractable.sliderDipendente.maxValue = durataInterazione;
        dipendenteOnInteractable.sliderDipendente.value = 0;

        while (timer < durataInterazione)
        {
            timer += Time.deltaTime;
            dipendenteOnInteractable.sliderDipendente.value = timer;
            yield return null;
        }

        timer = 0;
        timerIsOver = true;
        waitCoroutine = null;
    }
    #endregion
}