using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum InteractableType { TecaPopCorn, Spina, TecaPatatine, Cliente, StazioneDiRiposo, Scorta, Null }
public class Interactable : MonoBehaviour
{
    [Header("Interactable stuff")]
    public InteractableType type;
    public float durataInterazione;
    public Dipendente dipendenteOnInteractable;
    public int quantitaItemMassima;
    public int quantitaItem;
    [SerializeField] protected int quantitaDrop = 1;
    public NavMeshObstacle obstacle;
    public bool ignore;

    protected Item givenItem;
    private void Start()
    {
        if (type == InteractableType.TecaPopCorn || type == InteractableType.Scorta || type == InteractableType.Spina || type == InteractableType.TecaPatatine)
        {
            givenItem = GameManager.current.InteractableTypeToItem(type);
        }

        //quantitaItem = quantitaItemMassima;
    }

    Coroutine waitCoroutine;
    bool timerIsOver;
    public Status Interact(int azione) 
    {
        //Ho dovuto utilizzare una variabile di appoggio (timeIsOver) perché se invokavo le azioni dalla coroutine poi per farle
        //girare continuava a passare per la coroutine :( 

        /*if (waitCoroutine == null)
        {
            switch (azione) 
            {
                case 0:
                    waitCoroutine = StartCoroutine(WaitTime(Azione0));
                    break;
                case 1:
                    waitCoroutine = StartCoroutine(WaitTime(Azione1));
                    break;
            }
        }
        */

        if (waitCoroutine == null && !timerIsOver)
            waitCoroutine = StartCoroutine(WaitTime());

        if (timerIsOver)
        {
            switch (azione)
            {
                case 0:
                    return Azione0();
                case 1:
                    return Azione1();
            }
        }

        return Status.Running;
    }


    protected virtual Status Azione0() 
    { 
        timerIsOver = false;
        dipendenteOnInteractable.interazioneFinita = true; 
        /*Stato a caso sennò mi urla addosso */return Status.Running; 
    }

    protected virtual Status Azione1() 
    { 
        timerIsOver = false;
        dipendenteOnInteractable.interazioneFinita = true;
        /*Stato a caso sennò mi urla addosso */ return Status.Running;
    }

    float timer = 0;
    IEnumerator WaitTime()
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
}
