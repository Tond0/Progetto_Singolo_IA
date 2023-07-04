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

    //Quando � arrivato a destinazione...
    public bool arrivato;

    //Se la destinazione viene decisa allora non c'� bisogno di trovarne un'altra
    private bool destinationDecided;
    public override Status Process()
    {
        if (base.Process() == Status.Failure) return Status.Failure;

        #region Trova il tipo di interactable pi� vicino e libero.
        if (!destinationDecided) //Se la destinazione non � stata ancora decisa...
        {

            if(interactableType == InteractableType.Null) //Se deve capire da solo dove andare (come quando svolge un ordine(e quindi nella creazione nell tree mettiamo interactableType a NULL))...
                dipendente.targetInteractable = GameManager.current.GetFreeInteractable(dipendente.nextInteractableType, dipendente); //Cercher� l'areaItem che lui sta cercando (dipendente.nextInteractableType).

            else if(interactableType == InteractableType.Cliente && dipendente.cliente != null) //Se deve muoversi verso un cliente ma ne ha gi� uno, allora si muover� verso il suo cliente.
                dipendente.targetInteractable = dipendente.cliente;

            else //Se invece gli viene detto dalla creazione del tree dove deve andare...
                dipendente.targetInteractable = GameManager.current.GetFreeInteractable(interactableType, dipendente); //Lo facciamo andare dove noi abbiamo deciso precedentemente (interactableType)

            //Se � stato trovato un interactable libero che soddisfa i requisiti...
            if (dipendente.targetInteractable != null)
            {
                //Lo impostiamo come destinazione dell'agent.
                dipendente.agent.SetDestination(dipendente.targetInteractable.transform.position);
                
                //Lo disabilitiamo come ostacolo (senn� non ci arriver� mai l'agent)
                dipendente.targetInteractable.obstacle.enabled = false;
                
                //Settiamo a true il bool che ci eviter� di entrare di nuovo alla ricerca dell'interactable che serve al dipendente
                destinationDecided = true;

                //Segnamo quest'ultimo interactable come occupato. TODO: E se usassi delle action? Vedi nel polish.
                dipendente.targetInteractable.dipendenteOnInteractable = dipendente;
            }
        }
        #endregion

        #region Controllo se � arrivato o meno
        //Se l'ultimo interactable con cui � stato a contatto � il suo obbiettivo allora significa che � gi� arrivato a destinazione!
        if (dipendente.lastInteractable == dipendente.targetInteractable)
        {
            //Resettiamo a false il bool che ci permette di non far scegliere 20000 volte l'interactable
            destinationDecided = false;
            return Status.Success;
        }
        else
            return Status.Running;
        #endregion
    }
}
