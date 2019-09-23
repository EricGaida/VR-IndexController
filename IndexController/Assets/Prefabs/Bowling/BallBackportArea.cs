using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBackportArea : MonoBehaviour {
    public Transform backportPoint;
    private void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Interactable")) {
            collider.transform.position = backportPoint.position;
            collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
