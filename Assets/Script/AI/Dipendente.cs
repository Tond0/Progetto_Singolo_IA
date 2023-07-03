using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum Item { Niente, PopCorn, Bibita, Patatine }
public class Dipendente : MonoBehaviour
{

    #region Tree IA
    //Tree behaviour
    Radice root = new();

    //Il tipo di interactable con cui vuole interagire.
    public InteractableType nextInteractableType;

    //L'ultimo interagibile con cui è stato in contatto.
    public Interactable lastInteractable;
    //L'interactable del tipo InteractableType che questo dipendente sta cercando di raggiungere.
    public Interactable targetInteractable;

    //L'oggetto che sta trasportando.
    public List<Item> carryingItem;

    //Il cliente di cui si sta occupando.
    public Cliente cliente;

    //Ha finito di interagire con un'interagibile.
    public bool interazioneFinita;

    //Sta riposando?
    public bool staRiposando { get; set; }
    #endregion

    #region NavMesh IA
    //L'agent del dipendete
    public NavMeshAgent agent { get; private set; }
    #endregion

    #region UI
    public Slider sliderDipendente;
    #endregion

    Selettore selettore0 = new("Main selettore");
    // Start is called before the first frame update
    void Start()
    {
        #region Settaggio componenti
        //Todo: meglio non fare il get component e darglieli da inspector.
        agent = GetComponent<NavMeshAgent>();
        #endregion

        #region Creazione Albero       
        
        root.AddChild(selettore0);

        #region Prima ramificazione
        Sequenza sequenza00 = new Sequenza("Sequenza per riposare");
        C_TurnoFinito c_TurnoFinito = new C_TurnoFinito("Ha finito il turno?");
        Selettore selettore001 = new Selettore("1");
        C_Riposa c_staRiposando = new C_Riposa(this);
        Sequenza sequenza0000 = new Sequenza("b");
        A_MuovitiVersoInteractable a_muovitiVersoUscita = new A_MuovitiVersoInteractable(this, InteractableType.StazioneDiRiposo);
        A_Interagisci a_riposa = new A_Interagisci(this, InteractAction.Riposa);

        sequenza0000.AddChild(a_muovitiVersoUscita);
        sequenza0000.AddChild(a_riposa);

        selettore001.AddChild(c_staRiposando);
        selettore001.AddChild(sequenza0000);

        sequenza00.AddChild(c_TurnoFinito);
        sequenza00.AddChild(selettore001);

        selettore0.AddChild(sequenza00);
        #endregion

        #region Seconda ramificazione

        C_ServendoUnCliente c_staServendo = new(this);

        Sequenza sequenza000 = new("Sequenza per servire cliente");
        Selettore selettore0000 = new("Selettore per consegna o preparazione item");
        Sequenza sequenza00000 = new("Sequenza per continuare l'ordine");
        C_OrdineNonConcluso c_OrdineNonConcluso = new(this);
        A_MuovitiVersoInteractable a_muovitiVersoAreaItem = new(this, InteractableType.Null);
        A_Interagisci a_PreparaItem = new(this, InteractAction.PreparaItem);

        sequenza00000.AddChild(c_OrdineNonConcluso);
        sequenza00000.AddChild(a_muovitiVersoAreaItem);
        sequenza00000.AddChild(a_PreparaItem);

        selettore0000.AddChild(sequenza00000);

        Sequenza sequenza00001 = new("Sequenza per consegnare ordine");
        A_MuovitiVersoInteractable a_muovitiVersoCliente = new(this, InteractableType.Cliente);
        A_Interagisci a_ConsegnaOrdine = new(this, InteractAction.ConsegnaOrdine);

        sequenza00001.AddChild(a_muovitiVersoCliente);
        sequenza00001.AddChild(a_ConsegnaOrdine);

        selettore0000.AddChild(sequenza00001);

        sequenza000.AddChild(c_staServendo);
        sequenza000.AddChild(selettore0000);

        selettore0.AddChild(sequenza000);
        #endregion

        #region Terza ramificazione
        Sequenza sequenza02 = new("Sequenza per ordinazione");
        C_ClientiDisponibili c_ClientiDisponibili = new(this);
        A_MuovitiVersoInteractable a_muovitiVersoCliente1 = new(this, InteractableType.Cliente);
        A_Interagisci a_prendeOrdinazione = new(this, InteractAction.PrendiOrdinazione);

        sequenza02.AddChild(c_ClientiDisponibili);
        sequenza02.AddChild(a_muovitiVersoCliente1);
        sequenza02.AddChild(a_prendeOrdinazione);

        selettore0.AddChild(sequenza02);
        #endregion

        #endregion
    }

    private void Update()
    {
        root.Process();
        Debug.LogWarning("Il main selettore sta analizzando il nodo: " + selettore0.indexChild);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Se è a contatto con un interagibile e questo interagibile è ciò che voleva raggiungere allora è arrivato a destinazione!
        if(collision.transform.TryGetComponent(out Interactable interactable) && interactable == targetInteractable)
        {
            lastInteractable = interactable;
        }
    }
}
