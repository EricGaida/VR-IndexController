using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CanClimb : MonoBehaviour {
    [HideInInspector]
    public Vector3 previousPosition;
    [HideInInspector]
    public MyHand hand;
    [HideInInspector]
    public bool canGrip;
    [HideInInspector]
    public bool canSwingGrip;
    [HideInInspector]
    public Transform swing;

    void Start() {
        hand = GetComponent<MyHand>();
        previousPosition = hand.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Climb"))
            canGrip = true;
        if (other.CompareTag("ClimbSwing")) {
            canSwingGrip = true;
            swing = other.GetComponent<Transform>();
            Debug.Log(swing.name);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Climb"))
            canGrip = false;
        if (other.CompareTag("ClimbSwing")) {
            canSwingGrip = false;
            swing = null;
        }
    }
}
