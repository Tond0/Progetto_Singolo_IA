using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : Interactable
{
    private void Start()
    {
        type = InteractableType.Cliente;    
    }

    protected override void Azione()
    {
        InteractableType itemRichiesto = (InteractableType)Random.Range(0,2);
        dipendenteOnInteractable.nextInteractable = itemRichiesto;
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);   
    }
}
