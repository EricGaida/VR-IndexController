using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class MyInteractable : MonoBehaviour {
    public Vector3 Offset = Vector3.zero;
    public Vector3 AngularOffset = Vector3.zero;
    public SteamVR_ActionSet activateActionSet;
    public bool hideHandModelOnPickUp = false;

    [HideInInspector]
    public MyHand activeHand;

    public virtual void Action() {
        print("Action");
    }

    public virtual void OnPickup(MyHand hand) {
        // activate ActionSet
        if (activateActionSet != null) {
            activateActionSet.Activate(hand.handType);
        }
        // hide hand model
        if (hideHandModelOnPickUp) {
            hand.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public virtual void OnDrop(MyHand hand) {
        // deactivate ActionSet
        if (activateActionSet != null) {
            activateActionSet.Deactivate(hand.handType);
        }
        // enable Hand model
        if (hideHandModelOnPickUp) {
            hand.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void ApplyOffset(Transform hand) {
        transform.SetParent(hand);
        transform.localRotation = Quaternion.Euler(AngularOffset);
        transform.localPosition = Offset;
        transform.SetParent(null);
    }
}
