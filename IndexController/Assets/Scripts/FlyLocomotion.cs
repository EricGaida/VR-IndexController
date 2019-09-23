using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FlyLocomotion : MonoBehaviour {

    public SteamVR_Action_Boolean flyAcceleration;
    public SteamVR_Action_Pose flyDirection;
    public MyHand leftHand;
    public MyHand rightHand;
    public float flySpeed;

    private void FixedUpdate() {
        if (flyAcceleration.GetState(rightHand.handType))
            GetComponent<Rigidbody>().AddForce((GetHandRot(rightHand) * Vector3.forward) * flySpeed);
        if (flyAcceleration.GetState(leftHand.handType))
            GetComponent<Rigidbody>().AddForce((GetHandRot(leftHand) * Vector3.forward) * flySpeed);
    }

    public Quaternion GetHandRot(MyHand hand) {
        return flyDirection.GetLocalRotation(hand.handType);
    }
}
