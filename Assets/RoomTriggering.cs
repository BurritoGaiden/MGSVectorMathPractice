using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggering : MonoBehaviour {

    public delegate void TriggerAction (GameObject trigger);
    public static event TriggerAction GateTrigger;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Transition")
        {
            GateTrigger(other.gameObject);
        }
    }
}
