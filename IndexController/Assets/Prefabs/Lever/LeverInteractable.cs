using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteractable : MyInteractable {
    public Transform backportPoint;
    public GameObject visableHand;

    [HideInInspector]
    public bool isGrabbed;

    private void Awake() {
        if (!GetComponent<Rigidbody>().isKinematic)
            GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void OnPickup(MyHand hand) {
        isGrabbed = true;
        visableHand.SetActive(true);
        base.OnPickup(hand);
    }

    public override void OnDrop(MyHand hand) {
        transform.position = backportPoint.position;
        transform.rotation = backportPoint.rotation;
        isGrabbed = false;
        visableHand.SetActive(false);
        base.OnDrop(hand);
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Lever")) {
            if (isGrabbed)
                activeHand.Drop();
        }
    }
}
