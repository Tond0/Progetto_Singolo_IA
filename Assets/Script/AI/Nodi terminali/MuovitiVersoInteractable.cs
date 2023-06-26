using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MuovitiVersoInteractable : Nodo
{
    private readonly Dipendente dipendente;
    public MuovitiVersoInteractable(Dipendente dipendente)
    {
        this.dipendente = dipendente;
    }

    private Interactable targetInteractable;

    //Quando è arrivato a destinazione...
    public bool arrivato;
    public override Status Process()
    {
        if (targetInteractable == null)
        {
            targetInteractable = GameManager.current.GetFreeInteractable(dipendente.agent, dipendente.nextInteractable);

            if (targetInteractable != null)
            {
                dipendente.agent.SetDestination(targetInteractable.transform.position);

                targetInteractable.dipendenteOnInteractable = dipendente;
            }
            
            Debug.Log("MuovitiVersoItem: " + targetInteractable);
        }

        //Se l'interactable con cui sta interagendo è uguale a quello che si vuole raggiungere allora è arrivato a destinazione!
        if(dipendente.targetInteractable == this.targetInteractable)
            return Status.Success;
        else
            return Status.Running;
    }
}
