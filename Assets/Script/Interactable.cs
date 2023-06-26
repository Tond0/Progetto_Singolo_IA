using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractableType { TecaPopCorn, Spina, Cassa, Cliente }
public class Interactable : MonoBehaviour
{
    [Header("Interactable stuff")]
    public InteractableType type;
    public float durataInterazione;
    public Dipendente dipendenteOnInteractable;

    Coroutine waitCoroutine;
    public void Interact() 
    {
        if(waitCoroutine == null) waitCoroutine = StartCoroutine(WaitTime());
    }

    protected virtual void Azione() { }

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
            Debug.Log("Loading...");
            yield return null;
        }
        Debug.Log("Caricamento finito!");
        Azione();
    }

    protected virtual void OnCollisionEnter(Collision other) 
    {
        if (other.transform.TryGetComponent(out Dipendente dipendente))
        {
            if(dipendenteOnInteractable != dipendente) return;
            //Sta interagendo con questo interagibile!
            dipendente.targetInteractable = this;
        }
    }
}
