using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeActivator : MonoBehaviour, Interactable
{
    public DialougeObject dialougeObject;

    public void UpdateDialougeObject(DialougeObject dialougeObject)
    {
        this.dialougeObject = dialougeObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            player.interactable = this;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            if(player.interactable is DialougeActivator dialougeActivator && dialougeActivator == this)
            {
                player.interactable = null;
            }
        }
    }

    public void Interact(PlayerMovement player)
    {
        foreach(DialougeResponseEvent responseEvents in GetComponents<DialougeResponseEvent>())
        {
            if(responseEvents.dialougeObject == dialougeObject)
            {
                player.dialougeUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        player.dialougeUI.ShowDialouge(dialougeObject);
    }
}
