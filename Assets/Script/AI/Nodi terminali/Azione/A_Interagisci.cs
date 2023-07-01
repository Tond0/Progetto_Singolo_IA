using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractAction 
{
    PrendiOrdinazione,
    PreparaItem,
    PrendiScorta,
    ConsegnaOrdine,
    RifornisciPostazione,
    Riposa,
}
public class A_Interagisci : Nodo
{
    Dipendente dipendente;
    InteractableType thisInteractable;

    int actionToPerform;
    public A_Interagisci(Dipendente dipendente, InteractAction actionType) 
    {
        this.dipendente = dipendente;

        thisInteractable = dipendente.nextInteractable;

        switch (actionType)
        {
            case InteractAction.PrendiOrdinazione:
                actionToPerform = 0;
                break;

            case InteractAction.PreparaItem:
                actionToPerform = 0;
                break;

            case InteractAction.PrendiScorta:
                actionToPerform = 0;
                break;

            case InteractAction.ConsegnaOrdine: 
                actionToPerform = 1;
                break;

            case InteractAction.RifornisciPostazione:
                actionToPerform = 1;
                break;

            case InteractAction.Riposa:
                actionToPerform = 0;
                break;
        }
    }

    public override Status Process()
    {
        //Ascolta l'esigenze del cliente.
        dipendente.targetInteractable.Interact(actionToPerform);

        //Se il suo target interactable � ancora lo stesso significa che non ha ancora finito di interagire
        if (!dipendente.interazioneFinita)
            return Status.Running;
        else
        {
            dipendente.interazioneFinita = false;
            return Status.Success;
        }
    }
}