using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MuovitiVersoAreaItem : Nodo
{
    private readonly Dipendente dipendente;
    public MuovitiVersoAreaItem(Dipendente dipendente)
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
            switch (dipendente.itemRichiesto)
            {
                case TipoItem.PopCorn:
                    targetInteractable = GameManager.current.GetFreeInteractable(dipendente.agent, InteractableType.AreaPopCorn);
                    break;

                case TipoItem.Bibita:
                    targetInteractable = GameManager.current.GetFreeInteractable(dipendente.agent, InteractableType.Spina);
                    break;
            }

            if (targetInteractable != null)
            {
                dipendente.agent.SetDestination(targetInteractable.transform.position);

                dipendente.targetInteractable = targetInteractable;
            }
            
            Debug.Log("MuovitiVersoItem: " + targetInteractable);
        }

        if(arrivato)
            return Status.Success;
        else
            return Status.Running;
    }
}
