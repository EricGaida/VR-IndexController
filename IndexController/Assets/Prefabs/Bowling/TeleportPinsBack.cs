using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPinsBack : TriggerButton {
    public GameObject pins;

    public override void Action() {
        Debug.Log("action");
        foreach (Transform child in pins.transform)
            child.GetComponent<Pin>().ResetPosition();
    }
}