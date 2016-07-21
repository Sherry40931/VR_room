using UnityEngine;
using System.Collections;

public abstract class InteractableBase : MonoBehaviour
{
    // Default definition for buttons is NOOP
    public virtual void OnTriggerPressDown(Controller wand) { }
    public virtual void OnTriggerPressUp(Controller wand) { }
    public virtual void OnGripPressDown(Controller wand) { }
    public virtual void OnGripPressUp(Controller wand) { }
}