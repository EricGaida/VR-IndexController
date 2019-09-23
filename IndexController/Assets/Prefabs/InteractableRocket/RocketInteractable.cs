using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class RocketInteractable : MyInteractable {

    public GameObject flyingLocomotion;

    public override void Action() {
        // enables flying locomotion
        flyingLocomotion.GetComponent<FlyLocomotion>().enabled = true;
    }

    public override void OnPickup(MyHand hand) {
        base.OnPickup(hand);
        // hide picked up item
        GetComponent<MeshRenderer>().enabled = false;
        // enables flying locomotion
        flyingLocomotion.GetComponent<FlyLocomotion>().enabled = true;
        // enables rocket hand model
        hand.transform.GetChild(2).gameObject.SetActive(true);
    }

    public override void OnDrop(MyHand hand) {
        // enables picked up item
        GetComponent<MeshRenderer>().enabled = true;
        // disables flying locomotion
        flyingLocomotion.GetComponent<FlyLocomotion>().enabled = false;
        // disables rocket hand model
        hand.transform.GetChild(2).gameObject.SetActive(false);
        base.OnDrop(hand);
    }
}
