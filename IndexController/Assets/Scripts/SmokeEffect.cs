using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SmokeEffect : MonoBehaviour {

    public SteamVR_Action_Boolean accelerationAction;

    public MyHand hand;

    public GameObject prefabSmoke;

    void Update() {
        if (accelerationAction.GetState(hand.handType)) {
            GenSmoke();
        }
    }

    private void GenSmoke() {
        GameObject smoke = Instantiate(prefabSmoke, transform.position, Quaternion.identity);
        Destroy(smoke, 1.5f);
    }
}
