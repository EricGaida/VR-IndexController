using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake() {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Interactable"))
            GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized * 10);
    }

    public void ResetPosition() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
