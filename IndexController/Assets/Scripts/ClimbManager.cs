using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ClimbManager : MonoBehaviour {
    public Rigidbody body;
    public SteamVR_Action_Single grapAction;

    public ClimbingPull leftHand;
    public ClimbingPull rightHand;

    private float lastLeftGrap;
    private float lastRightGrap;

    private bool isGrappedLeft = false;
    private bool isGrappedRight = false;
    private bool isGrapped = false;

    void FixedUpdate() {
        if (isGrappedLeft || isGrappedRight)
            isGrapped = true;
        else
            isGrapped = false;

        if (!isGrapped)
            body.useGravity = true;

        // LeftHand
        if (leftHand.canGrip && GetGrap(leftHand.hand)) {
            body.velocity = Vector3.zero;
            body.useGravity = false;
            body.transform.position += (leftHand.previousPosition - leftHand.hand.transform.localPosition);
            isGrappedLeft = true;
        }
        else if (leftHand.canGrip && GetGrapUp(leftHand.hand, lastLeftGrap)) {
            body.useGravity = true;
            body.velocity = (leftHand.previousPosition - leftHand.hand.transform.localPosition) / Time.deltaTime;
        }
        else
            isGrappedLeft = false;

        // RightHand
        if (rightHand.canGrip && GetGrap(rightHand.hand)) {
            Debug.Log("STop");
            body.velocity = Vector3.zero;
            body.useGravity = false;
            body.transform.position += (rightHand.previousPosition - rightHand.hand.transform.localPosition);
            isGrappedRight = true;
        }
        else if (rightHand.canGrip && GetGrapUp(rightHand.hand, lastRightGrap) ) {
            Debug.Log("Fling");
            body.useGravity = true;
            body.velocity = (rightHand.previousPosition - rightHand.hand.transform.localPosition) / Time.deltaTime;
        }
        else
            isGrappedRight = false;

        lastLeftGrap = grapAction.GetAxis(leftHand.hand.handType);
        lastRightGrap = grapAction.GetAxis(rightHand.hand.handType);

        leftHand.previousPosition = leftHand.hand.transform.localPosition;
        rightHand.previousPosition = rightHand.hand.transform.localPosition;

    }

    private bool GetGrap(MyHand hand) {
        if (grapAction.GetAxis(hand.handType) > 0.8f)
            return true;
        else
            return false;
    }

    private bool GetGrapUp(MyHand hand, float lastGrap) {
        if (grapAction.GetAxis(hand.handType) < 0.8f && lastGrap > 0.8f)
            return true;
        return false;
    }
}
