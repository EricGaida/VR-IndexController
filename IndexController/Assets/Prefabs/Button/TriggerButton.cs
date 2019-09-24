﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : MonoBehaviour {
    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Button")) {
            Action();
        }
    }

    public virtual void Action() {
        Debug.Log("ButtonPressed!");
    }
}