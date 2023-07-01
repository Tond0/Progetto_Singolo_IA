using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractableType { TecaPopCorn, Spina, Cliente, StazioneDiRiposo }
public class Interactable : MonoBehaviour
{
    [Header("Interactable stuff")]
    public InteractableType type;
    public float durataInterazione;
    public Dipendente dipendenteOnInteractable;

    Coroutine waitCoroutine;
    public void Interact(int azione) 
    {
        if (waitCoroutine == null)
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
    }

    protected virtual void Azione0() { /*L'interazione con l'interactable è finita*/ dipendenteOnInteractable.interazioneFinita = true; }
    protected virtual void Azione1() { /*L'interazione con l'interactable è finita*/ dipendenteOnInteractable.interazioneFinita = true; }

    float timer = 0;
    IEnumerator WaitTime(Action azione)
    {
        //SetUp slider
        dipendenteOnInteractable.sliderDipendente.maxValue = durataInterazione;
        dipendenteOnInteractable.sliderDipendente.value = 0;

        while (timer < durataInterazione)
        {
            timer += Time.deltaTime;
            dipendenteOnInteractable.sliderDipendente.value = timer;
            Debug.Log("Loading...");
            yield return null;
        }
        Debug.Log("Caricamento finito!");

        azione.Invoke();
    }
}
