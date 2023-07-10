using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Bottoni a destra")]
    [SerializeField] private Button AggiungiBlu;
    [SerializeField] private Button AggiungiAzzurro;
    [SerializeField] private Button AggiungiViola;
    [SerializeField] private Button AggiungiCliente;
    [Header("Componenti per cambio shift")]
    [SerializeField] private Button CambiaShift;
    [SerializeField] private Image ShiftStatus;

    // Start is called before the first frame update
    void Start()
    {
        AggiungiBlu.onClick.AddListener(() => GameManager.current.AddArea(InteractableType.Spina));
        AggiungiViola.onClick.AddListener(() => GameManager.current.AddArea(InteractableType.TecaPopCorn));
        AggiungiAzzurro.onClick.AddListener(() => GameManager.current.AddArea(InteractableType.TecaPatatine));
        AggiungiCliente.onClick.AddListener(() => GameManager.current.AddArea(InteractableType.Cliente));
    }

    public void SwitchShiftColor()
    {
        if (!GameManager.current.ShiftIsOver)
            ShiftStatus.color = Color.red;
        else
            ShiftStatus.color = Color.green;
    }
}
