using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour {
    public GameObject pins;
    private List<Pin> allPins = new List<Pin>();

    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Button")) {

            foreach (Transform child in pins.transform) {
                child.GetComponent<Pin>().ResetPosition();
            }
        }
    }
}