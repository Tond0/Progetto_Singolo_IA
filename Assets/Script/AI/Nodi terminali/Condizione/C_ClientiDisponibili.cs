using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class C_ClientiDisponibili : Nodo
{
    private Dipendente dipendente;
    public C_ClientiDisponibili(Dipendente dipendente)
    {
        this.dipendente= dipendente;
    }

    public override Status Process()
    {
        List<Interactable> clientiDisponibili = GameManager.current.GetFreeInteractables(InteractableType.Cliente, dipendente);

        if (clientiDisponibili.Count > 0)
        {
            clientiDisponibili[0].obstacle.enabled = false;
            dipendente.targetInteractable = clientiDisponibili[0];
            return Status.Success;
            
            //Cose che non sono riuscito a fare...
            /*if (clientiDisponibili.Count == 1)
            {
                dipendente.targetInteractable = clientiDisponibili[0];
                return Status.Success;
            }
            else
            {
                Interactable clientePiuVicinoDisponibile = GameManager.current.FindNearest(clientiDisponibili, dipendente.agent);

                if (!clientePiuVicinoDisponibile)
                {
                    return Status.Running;
                }
                else
                {
                    dipendente.targetInteractable = clientePiuVicinoDisponibile;
                    return Status.Success;
                }
            }
            */
        }
        else
            return Status.Failure;
    }
}
