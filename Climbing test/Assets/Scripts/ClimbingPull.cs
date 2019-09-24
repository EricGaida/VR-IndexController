using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ClimbingPull : MonoBehaviour {
    [HideInInspector]
    public Vector3 previousPosition;
    [HideInInspector]
    public MyHand hand;
    [HideInInspector]
    public bool canGrip;

    void Start() {
        hand = GetComponent<MyHand>();
        previousPosition = hand.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Climb"))
            canGrip = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Climb"))
            canGrip = false;
    }

}
