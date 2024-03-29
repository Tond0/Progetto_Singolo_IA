using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum Item { Niente, PopCorn, Bibita, Patatine, Scorta }
public class Dipendente : MonoBehaviour
{
    #region Tree IA
    //Tree behaviour
    Radice root = new();

    //TODO: Da spostare nello start.
    Selettore mainSelector = new("Main selettore");

    //Il tipo di interactable con cui vuole interagire.
    [HideInInspector] public InteractableType nextInteractableType;


    [Header("Componenti")]
    public Slider sliderDipendente;

    [Header("Debug, da non modificare")]
    //L'ultimo interagibile con cui � stato in contatto.
    public Interactable lastInteractable;
    //L'interactable del tipo InteractableType che questo dipendente sta cercando di raggiungere.
    public Interactable targetInteractable;
    //Il cliente di cui si sta occupando.
    public Cliente cliente;
    //L'oggetto che sta trasportando.
    public List<Item> carryingItem;


    #region Bool di controllo
    //Si sta impegnando a rifornire una postazione?
    public bool StaRifornendo { get; set; }

    //Ha finito di interagire con un'interagibile.
    public bool InterazioneFinita { get; set; }

    //Sta riposando?
    public bool StaRiposando { get; set; }
    #endregion
    #endregion

    #region NavMesh IA
    //L'agent del dipendete
    public NavMeshAgent agent { get; private set; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Settaggio componenti
        //Todo: meglio non fare il get component e darglieli da inspector.
        agent = GetComponent<NavMeshAgent>();
        #endregion

        #region Creazione Albero       
        
        root.AddChild(mainSelector);

        #region Prima ramificazione (Ordine)

        #region Instanziamento
        /**************************************************/

        Selettore selettorePrimaRamificazione = new("Selettore prima ramificazione");

        /**************************************************/

        Sequenza sequenzaConCliente = new("Sequenza che si chiede se sta gi� servendo un cliente");
        
        Sequenza sequenzaSenzaCliente = new("Sequenza che si chiede se ci sono nuovi clienti");

        /**************************************************/

        Selettore selettoreStatoOrdine = new("Selettore che si chiede a che stato � l'ordine");

        C_ServendoUnCliente c_servendoUnCliente = new(this);

        /**************************************************/

        C_ClientiDisponibili c_clientiDisponibili = new(this);

        A_TrovaAreaLibera a_trovaClienteLibero = new(this, InteractableType.Cliente);

        A_MuovitiVersoInteractable a_muovitiVersoNuovoCliente = new(this);

        A_Interagisci a_prendeOrdinazione = new(this, InteractAction.PrendiOrdinazione);


        /**************************************************/

        C_OrdineNonConcluso c_OrdineNonConcluso = new(this, "L'ORDINE E' CONCLUSO?");

        Selettore selettoreContinuoOrdine = new("Selettore per continuo dell'ordine");

        /**************************************************/

        Sequenza sequenzaOrdineConcluso = new("Selettore che si chiede se � finito l'ordine");

        Sequenza sequenzaConsegnaOrdine = new("Sequenza per la consegna dell'ordine");

        /**************************************************/

        A_MuovitiVersoInteractable a_muovitiVersoCliente = new(this);

        A_Interagisci a_consegnaOrdine = new(this, InteractAction.ConsegnaOrdine);

        /**************************************************/

        //Si mette Null perch� non dobbiamo dirgli noi dove andare ma dovr� capirlo da solo!
        A_TrovaAreaLibera a_trovaAreaItem = new(this, InteractableType.Null);

        /**************************************************/

        Sequenza sequenzaPreparazioneItem = new("Sequenza per preparazione item");


        /**************************************************/

        Selettore selettoreControlloRisorse = new("Selettore per controllo di risorse sufficienti");

        /**************************************************/

        Sequenza sequenzaTrovaScorta = new("Sequenza per raccogliere scorta");

        /**************************************************/

        C_NonStaGiaRifornendo c_nonStaGiaRifornendo = new(this);

        A_MuovitiVersoInteractable a_muovitiVersoAreaItem = new(this);

        A_Interagisci a_preparaItem = new(this, InteractAction.PreparaItem);

        /**************************************************/

        A_TrovaAreaLibera a_trovaScortaLibera = new(this, InteractableType.Scorta);

        A_MuovitiVersoInteractable a_muovitiVersoScorta = new(this);

        A_Interagisci a_prendiScorta = new(this, InteractAction.PrendiScorta);

        /**************************************************/

        Sequenza sequenzaRifornimento = new("Sequenza che controlla se possiede una scorta");

        C_PossiedeScorta c_possiedeScorta = new(this);

        A_MuovitiVersoInteractable a_muovitiVersoPostazioneVuota = new(this);

        A_Interagisci a_riempiAreaItem = new(this, InteractAction.RifornisciPostazione); //Ricordati di mettere la condizione failure.
        #endregion

        #region Collegamento rami
        //CREAZIONE RAMIFICAZIONE
        /**************************************************/

        sequenzaRifornimento.AddChild(c_possiedeScorta);
        sequenzaRifornimento.AddChild(a_muovitiVersoPostazioneVuota);
        sequenzaRifornimento.AddChild(a_riempiAreaItem);

        sequenzaTrovaScorta.AddChild(a_trovaScortaLibera);
        sequenzaTrovaScorta.AddChild(a_muovitiVersoScorta);
        sequenzaTrovaScorta.AddChild(a_prendiScorta);

        selettoreControlloRisorse.AddChild(sequenzaRifornimento);
        selettoreControlloRisorse.AddChild(sequenzaTrovaScorta);

        /**************************************************/

        sequenzaPreparazioneItem.AddChild(a_trovaAreaItem);
        sequenzaPreparazioneItem.AddChild(a_muovitiVersoAreaItem);
        sequenzaPreparazioneItem.AddChild(c_nonStaGiaRifornendo);
        sequenzaPreparazioneItem.AddChild(a_preparaItem);

        /**************************************************/

        selettoreContinuoOrdine.AddChild(sequenzaPreparazioneItem);
        selettoreContinuoOrdine.AddChild(selettoreControlloRisorse);

        /**************************************************/

        sequenzaOrdineConcluso.AddChild(c_OrdineNonConcluso);
        sequenzaOrdineConcluso.AddChild(selettoreContinuoOrdine);

        /**************************************************/

        sequenzaConsegnaOrdine.AddChild(a_muovitiVersoCliente);
        sequenzaConsegnaOrdine.AddChild(a_consegnaOrdine);

        /**************************************************/

        selettoreStatoOrdine.AddChild(sequenzaOrdineConcluso);
        selettoreStatoOrdine.AddChild(sequenzaConsegnaOrdine);

        /**************************************************/

        sequenzaConCliente.AddChild(c_servendoUnCliente);
        sequenzaConCliente.AddChild(selettoreStatoOrdine);

        /**************************************************/

        sequenzaSenzaCliente.AddChild(c_clientiDisponibili);
        sequenzaSenzaCliente.AddChild(a_trovaClienteLibero);
        sequenzaSenzaCliente.AddChild(a_muovitiVersoNuovoCliente);
        sequenzaSenzaCliente.AddChild(a_prendeOrdinazione);

        /**************************************************/

        selettorePrimaRamificazione.AddChild(sequenzaConCliente);
        selettorePrimaRamificazione.AddChild(sequenzaSenzaCliente);

        /**************************************************/

        /* Primo try di albero
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

        mainSelector.AddChild(sequenza000);*/
        #endregion

        #endregion

        #region Seconda ramificazione (Rifornisci se non ci sono clienti)

        #region Instanziamento albero

        C_PostazioniDaRiempire c_postazioneDaRiempire = new(this);

        Sequenza sequenzaSecondaRamificazione = new("Sequenza che si chiede se le postazioni sono riempibili");

        Selettore selettorePossessoScorta = new("Selettore che si chiede se � in possesso di una scorta");

        Sequenza sequenzaCherRifornisce = new("Sequenza per il rifornimento di una stazione vuota");
        
        Sequenza sequenzaPerPrendereScorta = new("Sequenza per ottenere una scorta");

        #endregion

        #region Collegamento rami

        sequenzaPerPrendereScorta.AddChild(a_trovaScortaLibera);
        sequenzaPerPrendereScorta.AddChild(a_muovitiVersoAreaItem);
        sequenzaPerPrendereScorta.AddChild(a_prendiScorta);

        sequenzaCherRifornisce.AddChild(c_possiedeScorta);
        sequenzaCherRifornisce.AddChild(a_trovaAreaItem);
        sequenzaCherRifornisce.AddChild(a_muovitiVersoAreaItem);
        sequenzaCherRifornisce.AddChild(a_riempiAreaItem);

        selettorePossessoScorta.AddChild(sequenzaCherRifornisce);
        selettorePossessoScorta.AddChild(sequenzaPerPrendereScorta);

        sequenzaSecondaRamificazione.AddChild(c_postazioneDaRiempire);
        sequenzaSecondaRamificazione.AddChild(selettorePossessoScorta);

        #endregion

        #endregion

        #region Terza ramificazione (Riposo)
        Sequenza sequenzaTerzaRamificazione = new Sequenza("Sequenza per riposare");
            C_TurnoFinito c_TurnoFinito = new C_TurnoFinito("Ha finito il turno?");
            Selettore selettore001 = new Selettore("1");
            C_Riposa c_staRiposando = new C_Riposa(this);
            Sequenza sequenza0000 = new Sequenza("b");
            A_TrovaAreaLibera a_trovaStazioneRiposo = new(this, InteractableType.StazioneDiRiposo);
            A_MuovitiVersoInteractable a_muovitiVersoUscita = new A_MuovitiVersoInteractable(this);
            A_Interagisci a_riposa = new A_Interagisci(this, InteractAction.Riposa);

            sequenza0000.AddChild(a_trovaStazioneRiposo);
            sequenza0000.AddChild(a_muovitiVersoUscita);
            sequenza0000.AddChild(a_riposa);

            selettore001.AddChild(c_staRiposando);
            selettore001.AddChild(sequenza0000);

            sequenzaTerzaRamificazione.AddChild(c_TurnoFinito);
            sequenzaTerzaRamificazione.AddChild(selettore001);
        #endregion

        mainSelector.AddChild(selettorePrimaRamificazione);
        mainSelector.AddChild(sequenzaTerzaRamificazione);
        mainSelector.AddChild(sequenzaSecondaRamificazione);
        #endregion

    }

    private void Update()
    {
        root.Process();
        //Debug.LogWarning("Il main selettore sta analizzando il nodo: " + mainSelector.indexChild);
    }

    public Item NextItemInOrder()
    {
        if (!cliente) return Item.Niente;

        List<Item> ordineAppoggio = new(cliente.order);

        foreach (Item itemPreparato in carryingItem)
        {
            foreach (Item itemOrdinato in ordineAppoggio)
            {
                if (itemPreparato == itemOrdinato) { ordineAppoggio.Remove(itemOrdinato); break; }
            }
        }

        if (ordineAppoggio.Count <= 0)
            return Item.Niente;
        else
            return ordineAppoggio[0];
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Se � a contatto con un interagibile e questo interagibile � ci� che voleva raggiungere allora � arrivato a destinazione!
        if(collision.transform.TryGetComponent(out Interactable interactable) && interactable == targetInteractable)
        {
            lastInteractable = interactable;
        }
    }
}
