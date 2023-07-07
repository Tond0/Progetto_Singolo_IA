using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class C_PostazioniDaRiempire : Nodo
{
    readonly Dipendente dipendente;
    public C_PostazioniDaRiempire(Dipendente dipendente)
    {
        this.dipendente = dipendente;
    }

    public override Status Process()
    {
        if(base.Process() == Status.Failure) return Status.Failure;

        int interactableType = 0;

        while (interactableType < 3)
        {
            List<Interactable> areaDaRifornire = GameManager.current.GetFreeInteractables((InteractableType)interactableType, dipendente);

            if (areaDaRifornire.Count > 0)
            {
                foreach(Interactable i in areaDaRifornire)
                {
                    if(i.quantitaItem < i.quantitaItemMassima)
                    {
                        areaDaRifornire[0].dipendenteOnInteractable = dipendente;

                        dipendente.nextInteractableType = areaDaRifornire[0].type;

                        return Status.Success;
                    }
                }
            }

            interactableType++;
        }

        return Status.Failure;

    }
}
