using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ClimbingPull : MonoBehaviour {
    public Transform Body;
    public SteamVR_Action_Boolean gripAction;
    public MyHand hand;
    public SteamVR_TrackedObject controller;

    [HideInInspector]
    public Vector3 previousPosition;


    void Start() {
        hand = GetComponent<MyHand>();
        previousPosition = hand.transform.localPosition;
    }
    

    void Update() {
        if (gripAction.GetState(hand.handType)) {
            Body.transform.position += (previousPosition - controller.transform.position);

        }        
    }
}
