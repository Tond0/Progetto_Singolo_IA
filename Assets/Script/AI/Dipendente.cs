using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Dipendente : MonoBehaviour
{
    //Tree behaviour
    Radice root = new();

    //Dipendente stuff
    //L'oggetto con cui sta interagendo.
    public Interactable targetInteractable;
    //L'agent del dipendete
    public NavMeshAgent agent { get; private set; }
    //L'interactable con cui sta interagento
    public InteractableType nextInteractable;
    //L'interactable controllerà e dirà se il dipendete è arrivato a destinazione (nel suo collider)
    public bool arrivedToInteractable { get; set; }

    public Slider sliderDipendente;

    // Start is called before the first frame update
    void Start()
    {
        #region Settaggio componenti
        //Todo: meglio non fare il get component e darglieli da inspector.
        agent = GetComponent<NavMeshAgent>();
        #endregion

        nextInteractable = InteractableType.Cliente;

        #region Creazione Albero
        Sequenza sequenza0 = new();

        /*Selettore selettore00 = new();
        Condizione condizione00 = new Condizione("Sta servendo un cliente?", false);
        PrendiOrdinazione aApprociaCliente = new();

        Selettore selettore01 = new();
        Condizione condizione010 = new Condizione("Il cliente ha ricevuto tutto?", false);
        Sequenza sequenza011 = new Sequenza();
        MuovitiVersoInteractable aPreparaItem = new(this);
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

        MuovitiVersoInteractable aMuovitiVersoItem = new(this);
        PrendiOrdinazione aPrendiOrdinazione = new(this);
        MuovitiVersoInteractable aMuovitiVersoItem1 = new(this);
        
        sequenza0.AddChild(aMuovitiVersoItem);
        sequenza0.AddChild(aPrendiOrdinazione);
        sequenza0.AddChild(aMuovitiVersoItem1);

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
