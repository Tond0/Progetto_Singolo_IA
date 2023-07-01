using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum Item { Niente, PopCorn, Bibita }
public class Dipendente : MonoBehaviour
{

    #region Tree IA
    //Tree behaviour
    Radice root = new();

    //Il tipo di interactable con cui vuole interagire.
    [NonSerialized] public InteractableType nextInteractable;

    //L'interactable del tipo InteractableType che questo dipendente sta cercando di raggiungere.
    public Interactable targetInteractable;

    //L'oggetto che sta trasportando.
    public Item carryingItem;

    //E' arrivato alla destinazione che gli era indicata?
    public bool arrivatoADestinazione { get; set; }
    //Ha finito di interagire con un'interagibile.
    public bool interazioneFinita { get; set; }

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

    // Start is called before the first frame update
    void Start()
    {
        #region Settaggio componenti
        //Todo: meglio non fare il get component e darglieli da inspector.
        agent = GetComponent<NavMeshAgent>();
        #endregion

        #region Creazione Albero        
        Sequenza sequenza00 = new Sequenza();
        C_TurnoFinito c_TurnoFinito = new C_TurnoFinito();
        Selettore selettore001 = new Selettore();
        C_Riposa c_staRiposando = new C_Riposa(this);
        Sequenza sequenza0000 = new Sequenza();
        A_MuovitiVersoInteractable a_muovitiVersoUscita = new A_MuovitiVersoInteractable(this, InteractableType.StazioneDiRiposo);
        A_Interagisci a_riposa = new A_Interagisci(this, InteractAction.Riposa);

        sequenza0000.AddChild(a_muovitiVersoUscita);
        sequenza0000.AddChild(a_riposa);

        selettore001.AddChild(c_staRiposando);
        selettore001.AddChild(sequenza0000);

        sequenza00.AddChild(c_TurnoFinito);
        sequenza00.AddChild(selettore001);

        root.AddChild(sequenza00);
        #endregion
    }

    bool onlyonce = true;
    private void Update()
    {
        root.Process();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Se è a contatto con un interagibile e questo interagibile è ciò che voleva raggiungere allora è arrivato a destinazione!
        if(collision.transform.TryGetComponent(out Interactable interactable) && interactable == targetInteractable)
        {
            arrivatoADestinazione = true;
        }
    }
}
