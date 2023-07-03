using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractableType { TecaPopCorn, Spina, TecaPatatine, Cliente, StazioneDiRiposo, Null }
public class Interactable : MonoBehaviour
{
    [Header("Interactable stuff")]
    public InteractableType type;
    public float durataInterazione;
    public Dipendente dipendenteOnInteractable;
    public bool ignore;

    Coroutine waitCoroutine;
    bool timerIsOver;
    public void Interact(int azione) 
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
                    Azione0();
                    break;
                case 1:
                    Azione1();
                    break;
            }
        }
    }

    protected Item givenItem;
    private void Start()
    {
        givenItem = InteractableTypeToItem(type);
    }

    #region TODO: CONTROLLA CHE LE HAI EFFETTIVAMENTE USATE STE FUNZIONI, E NON SOLO IN QUESTO START....
    protected Item InteractableTypeToItem(InteractableType interactableType)
    {
        switch (interactableType)
        {
            case InteractableType.TecaPopCorn:
                return Item.PopCorn;

            case InteractableType.Spina:
                return Item.Bibita;

            case InteractableType.TecaPatatine:
                return Item.Patatine;
        }

        Debug.LogError("Stai cercando un area che non da un item!");
        return Item.Niente;
    }

    protected InteractableType ItemToInteractableType(Item item) 
    {
        switch (item)
        {
            case Item.Bibita:
                return InteractableType.Spina;

            case Item.Patatine:
                return InteractableType.TecaPatatine;

            case Item.PopCorn:
                return InteractableType.TecaPopCorn;
        }
        Debug.LogError("Stai cercando un item che non ha un area!");
        return InteractableType.Null;
    }

    #endregion

    protected virtual void Azione0() { timerIsOver = false; dipendenteOnInteractable.interazioneFinita = true; }
    protected virtual void Azione1() { timerIsOver = false; dipendenteOnInteractable.interazioneFinita = true; }

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
