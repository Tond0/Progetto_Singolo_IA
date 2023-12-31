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
    readonly Dipendente dipendente;

    readonly int actionToPerform;
    public A_Interagisci(Dipendente dipendente, InteractAction actionType) 
    {
        this.dipendente = dipendente;

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
        if (base.Process() == Status.Failure) return Status.Failure;

        //Ascolta l'esigenze del cliente.
        Status statoInterazione = dipendente.targetInteractable.Interact(actionToPerform);

        //Se il suo target interactable � ancora lo stesso significa che non ha ancora finito di interagire
        if (!dipendente.InterazioneFinita)
        {
            return Status.Running;
        }
        else
        {
            dipendente.InterazioneFinita = false;

            if (statoInterazione == Status.Running)
                return Status.Success;

            return statoInterazione;
        }
    }
}
