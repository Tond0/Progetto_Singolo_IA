using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType { AreaPopCorn, Spina, Cassa }
public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable stuff")]
    public InteractableType type;
    public bool occupied;
    protected abstract void Interact();

    protected abstract void OnCollisionEnter(Collision other);
}
