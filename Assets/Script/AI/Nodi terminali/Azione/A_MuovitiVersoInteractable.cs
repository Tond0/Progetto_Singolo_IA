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

    //Quando � arrivato a destinazione...
    public bool arrivato;

    //Se la destinazione viene decisa allora non c'� bisogno di trovarne un'altra
    private bool destinationDecided;
    public override Status Process()
    {
        if (base.Process() == Status.Failure) return Status.Failure;

        #region Move
        if (!destinationDecided)
        {
            //Lo impostiamo come destinazione dell'agent.
            dipendente.agent.SetDestination(dipendente.targetInteractable.transform.position);

            //Non continua a settare la destinazione
            destinationDecided = true;
        }
        #endregion

        float sqrDistance = (dipendente.transform.position - dipendente.targetInteractable.transform.position).sqrMagnitude;
        #region Controllo se � arrivato o meno
        //Se l'ultimo interactable con cui � stato a contatto � il suo obbiettivo allora significa che � gi� arrivato a destinazione!
        if (sqrDistance <= 4)
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
