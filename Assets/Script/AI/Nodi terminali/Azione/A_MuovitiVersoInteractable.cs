using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class A_MuovitiVersoInteractable : Nodo
{
    private readonly Dipendente dipendente;
    private InteractableType interactableType;
    public A_MuovitiVersoInteractable(Dipendente dipendente, InteractableType interactableType)
    {
        this.dipendente = dipendente;
        this.interactableType = interactableType;
    }

    //Quando è arrivato a destinazione...
    public bool arrivato;

    //Se la destinazione viene decisa allora non c'è bisogno di trovarne un'altra
    private bool destinationDecided;
    public override Status Process()
    {
        if (base.Process() == Status.Failure) return Status.Failure;

        #region Trova il tipo di interactable più vicino e libero.
        if (!destinationDecided)
        {
            Debug.LogError(dipendente.nextInteractableType);

            if(interactableType == InteractableType.Null) //Se deve capire da solo dove andare...
                dipendente.targetInteractable = GameManager.current.GetFreeInteractable(dipendente.nextInteractableType, dipendente);
            else if(interactableType == InteractableType.Cliente && dipendente.cliente != null) //Se deve muoversi verso un cliente ma ne ha già uno, allora si muoverà verso il suo cliente.
                dipendente.targetInteractable = dipendente.cliente;
            else
                dipendente.targetInteractable = GameManager.current.GetFreeInteractable(interactableType, dipendente);


            if (dipendente.targetInteractable != null)
            {
                
                dipendente.agent.SetDestination(dipendente.targetInteractable.transform.position);

                destinationDecided = true;

                dipendente.targetInteractable.dipendenteOnInteractable = dipendente;
            }
        }
        #endregion

        if (dipendente.lastInteractable == dipendente.targetInteractable)
        {
            Debug.LogError("Arrivato");
            destinationDecided = false;
            return Status.Success;
        }
        else
            return Status.Running;
    }
}
