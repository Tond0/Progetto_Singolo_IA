using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class A_MuovitiVersoInteractable : Nodo
{
    private readonly Dipendente dipendente;
    public A_MuovitiVersoInteractable(Dipendente dipendente)
    {
        this.dipendente = dipendente;
    }

    //Quando è arrivato a destinazione...
    public bool arrivato;

    //Se la destinazione viene decisa allora non c'è bisogno di trovarne un'altra
    private bool destinationDecided;
    public override Status Process()
    {
        if (base.Process() == Status.Failure) return Status.Failure;

        #region Move
        if (!destinationDecided)
        {
            //Lo impostiamo come destinazione dell'agent.
            dipendente.agent.SetDestination(dipendente.targetInteractable.transform.position);

            //Segnamo quest'ultimo interactable come occupato. TODO: E se usassi delle action? Vedi nel polish.
            dipendente.targetInteractable.dipendenteOnInteractable = dipendente;

            //Non continua a settare la destinazione
            destinationDecided = true;
        }
        #endregion


        #region Controllo se è arrivato o meno
        //Se l'ultimo interactable con cui è stato a contatto è il suo obbiettivo allora significa che è già arrivato a destinazione!
        if (dipendente.lastInteractable == dipendente.targetInteractable)
        {
            //Reset.
            destinationDecided = false;

            return Status.Success;
        }
        else
            return Status.Running;
        #endregion
    }
}
