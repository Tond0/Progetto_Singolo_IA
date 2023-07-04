using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffWorkStation : Interactable
{
    protected override void Azione0()
    {
        dipendenteOnInteractable.staRiposando = true;
        Debug.Log("Dorme");

        base.Azione0();
    }
}
