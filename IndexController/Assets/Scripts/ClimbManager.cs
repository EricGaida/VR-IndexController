using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ClimbManager : MonoBehaviour {
    public SteamVR_Action_Single grapAction;
    public SteamVR_Action_Pose handPosition;

    public ClimbingPull leftHand;
    public ClimbingPull rightHand;

    [Range(0f, 1f)]
    public float grapStrengthTreshhold;

    private Rigidbody body;
    private float lastLeftGrap;
    private float lastRightGrap;
    private bool isGrappedLeft = false;
    private bool isGrappedRight = false;
    private bool isGrapped = false;

    private void Start() {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        // tests if a hand is still grabbed onto an object
        if (isGrappedLeft || isGrappedRight)
            isGrapped = true;
        else
            isGrapped = false;

        if (!isGrapped)
            body.useGravity = true;

        // LeftHand 
        if (leftHand.canGrip && GetGrap(leftHand.hand)) {
            // change to grab physics
            body.velocity = Vector3.zero;
            body.useGravity = false;
            isGrappedLeft = true;
            // calc grab position
            body.transform.position += (leftHand.previousPosition - handPosition.GetLocalPosition(leftHand.hand.handType));
        }
        else if (leftHand.canGrip && GetGrapUp(leftHand.hand, lastLeftGrap)) {
            // change to normal physics
            body.useGravity = true;
            isGrappedLeft = false;
            // calc fling velocity
            body.velocity = (leftHand.previousPosition - handPosition.GetLocalPosition(leftHand.hand.handType)) / Time.deltaTime;
        }
        else // has to be here because Hand can lose grap without letting go
            isGrappedLeft = false;

        // RightHand
        if (rightHand.canGrip && GetGrap(rightHand.hand)) {
            // change to grab physics
            body.velocity = Vector3.zero;
            body.useGravity = false;
            isGrappedRight = true;
            // calc grab position
            body.transform.position += (rightHand.previousPosition - handPosition.GetLocalPosition(rightHand.hand.handType));
        }
        else if (rightHand.canGrip && GetGrapUp(rightHand.hand, lastRightGrap)) {
            // change to normal physics
            body.useGravity = true;
            isGrappedRight = false;
            // calc fling velocity
            body.velocity = (rightHand.previousPosition - handPosition.GetLocalPosition(rightHand.hand.handType)) / Time.deltaTime;
        }
        else // has to be here because Hand can lose grap without letting go
            isGrappedRight = false;

        // set last grap power
        lastLeftGrap = grapAction.GetAxis(leftHand.hand.handType);
        lastRightGrap = grapAction.GetAxis(rightHand.hand.handType);

        // set previous position
        leftHand.previousPosition = handPosition.GetLocalPosition(leftHand.hand.handType);
        rightHand.previousPosition = handPosition.GetLocalPosition(rightHand.hand.handType);
    }

    // get if the grap condition has been met
    private bool GetGrap(MyHand hand) {
        if (grapAction.GetAxis(hand.handType) > grapStrengthTreshhold)
            return true;
        else
            return false;
    }

    // get if the letting grap go condition has been met
    private bool GetGrapUp(MyHand hand, float lastGrap) {
        if (grapAction.GetAxis(hand.handType) < grapStrengthTreshhold && lastGrap > grapStrengthTreshhold)
            return true;
        return false;
    }
}
