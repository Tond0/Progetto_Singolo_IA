using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class Cliente : Interactable
{
    private List<Item> order = new();

    private void Start()
    {
        type = InteractableType.Cliente;    
    }

    /*private List<Item> SpawnOrder()
    {
        List<Item> order = new List<Item>();

        int quantita = Random.Range(GameManager.current.grandezzaOrdineMinima, GameManager.current.grandezzaOrdineMinima + 1);

        for(int i = 0; i < quantita; i++)
        {

        }
    }*/
    

    //Azione0 = Prendere ordinazione
    protected override void Azione0()
    {
        //Allora significa che deve ancora sapere cosa portare al cliente
        InteractableType itemRichiesto = (InteractableType)Random.Range(0, 2);
        dipendenteOnInteractable.nextInteractable = itemRichiesto;
        
        base.Azione0();
    }

    //Azione1 = Consegna ordine
    protected override void Azione1()
    {
        base.Azione1();
    }
}
