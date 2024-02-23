using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class A_TrovaAreaLibera : Nodo
{
    readonly Dipendente dipendente;
    readonly InteractableType interactableType;
    private Status NavMeshUpdateStatus;
    public A_TrovaAreaLibera(Dipendente dipendente, InteractableType interactableType) : base()
    {
        this.dipendente = dipendente;
        this.interactableType = interactableType;
    }

    public override Status Process()
    {
        if (base.Process() == Status.Failure) return Status.Failure;

        List<Interactable> areeLibere = new();

        switch (interactableType)
        {
            case InteractableType.Null:

                //Se deve capire da solo dove andare (come quando svolge un ordine(e quindi nella creazione nell tree mettiamo interactableType a NULL))...
                areeLibere = GameManager.current.GetFreeInteractables(dipendente.nextInteractableType, dipendente);

                break;

            case InteractableType.Cliente:

                //Se deve muoversi verso un cliente ma ne ha gi� uno, allora si muover� verso il suo cliente.
                if (dipendente.cliente != null)
                    dipendente.targetInteractable = dipendente.cliente;
                else
                    areeLibere = GameManager.current.GetFreeInteractables(interactableType, dipendente);

                break;

            default:

                //Lo facciamo andare dove noi abbiamo deciso precedentemente (interactableType)
                areeLibere = GameManager.current.GetFreeInteractables(interactableType, dipendente);

                break;
        }

        if (areeLibere.Count > 0)
        {
            if (areeLibere[0].dipendenteOnInteractable == dipendente || areeLibere[0].dipendenteOnInteractable == null)
            {
                dipendente.targetInteractable = areeLibere[0];
                areeLibere[0].dipendenteOnInteractable = dipendente;
                return Status.Success;
            }

            //Cose che ho provato a fare ma non andavano...
            /*if (areeLibere.Count == 1)
            {
                dipendente.targetInteractable = areeLibere[0];
                return Status.Success;
            }
            else
            {
                Interactable areaLiberaPiuVicina = GameManager.current.FindNearest(areeLibere, dipendente.agent);

                if (!areaLiberaPiuVicina)
                {
                    return Status.Running;
                }
                else
                {
                    dipendente.targetInteractable = areaLiberaPiuVicina;
                    return Status.Success;
                }
            }
            */

        }
            return Status.Running;
    }
}
