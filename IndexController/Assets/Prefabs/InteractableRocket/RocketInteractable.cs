using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class RocketInteractable : MyInteractable {

    public override void OnPickup(MyHand hand) {
        base.OnPickup(hand);
        // hide picked up item
        GetComponent<MeshRenderer>().enabled = false;
        // enables rocket hand model
        hand.transform.GetChild(2).gameObject.SetActive(true);
    }

    public override void OnDrop(MyHand hand) {
        // enables picked up item
        GetComponent<MeshRenderer>().enabled = true;
        // disables rocket hand model
        hand.transform.GetChild(2).gameObject.SetActive(false);
        base.OnDrop(hand);
    }
}
