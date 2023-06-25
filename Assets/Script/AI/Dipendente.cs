using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum TipoItem { PopCorn, Bibita }

public class Dipendente : MonoBehaviour
{
    //Tree behaviour
    Radice root = new();

    //Dipendente stuff
    //L'oggetto con cui vuole o sta interagendo.
    public Interactable targetInteractable;
    //L'agent del dipendete
    public NavMeshAgent agent { get; private set; }
    //Item che il cliente chiederà al dipendente
    public TipoItem itemRichiesto { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        itemRichiesto = TipoItem.PopCorn;

        #region Creazione Albero
        Sequenza sequenza0 = new();

        /*Selettore selettore00 = new();
        Condizione condizione00 = new Condizione("Sta servendo un cliente?", false);
        ApprocciaCliente aApprociaCliente = new();

        Selettore selettore01 = new();
        Condizione condizione010 = new Condizione("Il cliente ha ricevuto tutto?", false);
        Sequenza sequenza011 = new Sequenza();
        MuovitiVersoAreaItem aPreparaItem = new(this);
        PortaItem aPortaItem = new();

        sequenza011.AddChild(aPreparaItem);
        sequenza011.AddChild(aPortaItem);

        selettore01.AddChild(condizione010);
        selettore01.AddChild(sequenza011);

        selettore00.AddChild(condizione00);
        selettore00.AddChild(aApprociaCliente);

        sequenza0.AddChild(selettore00);
        sequenza0.AddChild(selettore01);
        */

        MuovitiVersoAreaItem aMuovitiVersoItem = new(this);
        sequenza0.AddChild(aMuovitiVersoItem);

        root.AddChild(sequenza0);
        #endregion
        

    }

    bool onlyonce = true;
    private void Update()
    {
        if (onlyonce && root.Process() == Status.Success)
        {
            onlyonce= false;
        }
    }
}
