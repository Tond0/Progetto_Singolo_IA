using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class Cliente : Interactable
{
    public List<Item> order;

    private void Start()
    {
        type = InteractableType.Cliente;
        order = SpawnOrder();
    }

    #region Spawn dell'ordine
    private List<Item> SpawnOrder()
    {
        List<Item> randomOrder = new List<Item>();

        int quantita = Random.Range(GameManager.current.grandezzaOrdineMinima, GameManager.current.grandezzaOrdineMassima + 1);

        for(int i = 0; i < quantita; i++)
        {
            randomOrder.Add(SpawnItem());
        }

        return randomOrder;
    }

    private Item SpawnItem()
    {
        Item randomItem = (Item)Random.Range(1,4);
        return randomItem;
    }
    #endregion

    //Azione0 = Prendere ordinazione
    protected override void Azione0()
    {
        base.Azione0();

        //Che cliente sta servendo il dipendente?
        dipendenteOnInteractable.cliente = this;
        
    }

    //Azione1 = Consegna ordine
    protected override void Azione1()
    {

        //TODO: Uscita del cliente!

        dipendenteOnInteractable.cliente = null;
        ignore = true;
        dipendenteOnInteractable.carryingItem.Clear();
        
        base.Azione1();
        
        this.enabled = false;
    }
}
