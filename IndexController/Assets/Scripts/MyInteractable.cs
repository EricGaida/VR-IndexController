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
    public bool hideControllerModelOnPickUp = false;

    [HideInInspector]
    public MyHand activeHand;

    [HideInInspector]
    public SteamVR_Skeleton_Poser skeletonPoser;

    protected float blendToPoseTime = 0.1f;
    protected float releasePoseBlendTime = 0.2f;

    private void Awake() {
        skeletonPoser = GetComponent<SteamVR_Skeleton_Poser>();
        //renderer = GetComponentInChildren<Animator>().GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public virtual void Action() {
        print("Action");
    }

    public virtual void OnPickup(MyHand hand) {
        // activate ActionSet
        if (activateActionSet != null) {
            activateActionSet.Activate(hand.handType);
        }
        // hide controller model
        if (hideControllerModelOnPickUp) {
            hand.controllerRenderer.SetMeshRendererState(false);
        }
        // hide hand model
        if (hideHandModelOnPickUp) {
            hand.handRenderer.enabled = false;
        }
        // blend to poser skeleton
        if (skeletonPoser != null && hand.skeleton != null) {
            hand.skeleton.BlendToPoser(skeletonPoser);
        }

    }

    public virtual void OnDrop(MyHand hand) {
        // deactivate ActionSet
        if (activateActionSet != null) {
            activateActionSet.Deactivate(hand.handType);
        }
        // enable controller model
        if (hideControllerModelOnPickUp) {
            hand.controllerRenderer.SetMeshRendererState(true);
        }
        // enable hand model
        if (hideHandModelOnPickUp) {
            hand.handRenderer.enabled = true;
        }

        // blend to normal skeleton
        if (skeletonPoser != null) {
            if (hand.skeleton != null)
                hand.skeleton.BlendToSkeleton(releasePoseBlendTime);
        }
    }

    public void ApplyOffset(Transform hand) {
        transform.SetParent(hand);
        transform.localRotation = Quaternion.Euler(AngularOffset);
        transform.localPosition = Offset;
        transform.SetParent(null);
    }
}
