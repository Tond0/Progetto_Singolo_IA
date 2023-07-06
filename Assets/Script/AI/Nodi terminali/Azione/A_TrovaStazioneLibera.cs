using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_TrovaStazioneLibera : Nodo
{
    readonly Dipendente dipendente;
    readonly InteractableType interactableType;
    public A_TrovaStazioneLibera(Dipendente dipendente, InteractableType interactableType) : base()
    {
        this.dipendente = dipendente;
        this.interactableType = interactableType;
    }

    public override Status Process()
    {
        if (base.Process() == Status.Failure) return Status.Failure;

        switch (interactableType)
        {
            case InteractableType.Null:

                //Se deve capire da solo dove andare (come quando svolge un ordine(e quindi nella creazione nell tree mettiamo interactableType a NULL))...
                dipendente.targetInteractable = GameManager.current.GetFreeInteractable(dipendente.nextInteractableType, dipendente);

                break;

            case InteractableType.Cliente:
                
                //Se deve muoversi verso un cliente ma ne ha già uno, allora si muoverà verso il suo cliente.
                if (dipendente.cliente != null)
                    dipendente.targetInteractable = dipendente.cliente;
                else
                    dipendente.targetInteractable = GameManager.current.GetFreeInteractable(interactableType, dipendente);

                break;

            default:

                //Lo facciamo andare dove noi abbiamo deciso precedentemente (interactableType)
                dipendente.targetInteractable = GameManager.current.GetFreeInteractable(interactableType, dipendente);

                break;
        }

        //Se trova una stazione libera
        if (dipendente.targetInteractable != null)
            return Status.Success;

        //Se non trova una stazione libera
        else
            return Status.Running;
    }
}
