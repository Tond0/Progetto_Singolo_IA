using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffWorkStation : Interactable
{
    protected override Status Azione0()
    {
        base.Azione0();

        dipendenteOnInteractable.staRiposando = true;

        return Status.Success;
    }
}
