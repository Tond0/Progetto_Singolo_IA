using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class A_MuovitiVersoInteractable : Nodo
{
    private readonly Dipendente dipendente;
    private readonly InteractableType interactableType;
    public A_MuovitiVersoInteractable(Dipendente dipendente, InteractableType interactableType)
    {
        this.dipendente = dipendente;
        this.interactableType = interactableType;
    }

    //Quando è arrivato a destinazione...
    public bool arrivato;
    public override Status Process()
    {
        #region Trova il tipo di interactable più vicino e libero.
        if (dipendente.targetInteractable == null)
        {
            dipendente.targetInteractable = GameManager.current.GetFreeInteractable(dipendente.agent, interactableType);

            if (dipendente.targetInteractable != null)
            {
                dipendente.agent.SetDestination(dipendente.targetInteractable.transform.position);

                dipendente.targetInteractable.dipendenteOnInteractable = dipendente;
            }
            
            Debug.Log("MuovitiVersoItem: " + dipendente.targetInteractable);
        }
        #endregion

        if (dipendente.arrivatoADestinazione)
        {
            dipendente.arrivatoADestinazione = false;
            return Status.Success;
        }
        else
            return Status.Running;
    }
}
