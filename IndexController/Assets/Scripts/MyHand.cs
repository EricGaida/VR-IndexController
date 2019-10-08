using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MyHand : MonoBehaviour {

    public SteamVR_Action_Boolean pickupAction;
    public SteamVR_Action_Boolean dropAction;
    public SteamVR_Action_Boolean useAction;
    public SteamVR_Input_Sources handType;

    [HideInInspector]
    public SteamVR_Behaviour_Skeleton skeleton;
    [HideInInspector]
    public SkinnedMeshRenderer handRenderer;
    [HideInInspector]
    public SteamVR_RenderModel controllerRenderer;

    private SteamVR_Behaviour_Pose pose;
    private FixedJoint joint;

    private MyInteractable currentInteractable;
    private List<MyInteractable> inRangeInteractables = new List<MyInteractable>();


    private void Awake() {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();
        skeleton = GetComponentInChildren<SteamVR_Behaviour_Skeleton>();
        Debug.Log(skeleton.name);
        handRenderer = skeleton.GetComponentInChildren<Animator>().GetComponentInChildren<SkinnedMeshRenderer>();
        controllerRenderer = GetComponentInChildren<SteamVR_RenderModel>();
    }

    void Update() {
        // Pickup
        if (pickupAction.GetStateDown(pose.inputSource)) {
            if (currentInteractable != null) {
                currentInteractable.Action();
                return;
            }
            Pickup();
        }
        // Drop
        if (dropAction.GetStateUp(pose.inputSource)) {
            Drop();
        }

        // Action, use Interactable function
        if (useAction.GetState(pose.inputSource) && currentInteractable != null) {
            currentInteractable.Action();
        }
    }

    public void Pickup() {
        // Get nearest interactable
        currentInteractable = GetNearestInteractable();

        // Null check
        if (!currentInteractable)
            return;

        // check if kinematic or physics based
        if (currentInteractable.GetComponent<Rigidbody>().isKinematic) {
            currentInteractable.transform.parent = this.transform;
        }
        else {
            // Position
            currentInteractable.ApplyOffset(transform);

            // Attach
            Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
            joint.connectedBody = targetBody;
        }
        // Set active hand
        currentInteractable.activeHand = this;
        currentInteractable.OnPickup(this);
    }

    public void Drop() {
        // Null check
        if (!currentInteractable)
            return;

        // Method called before drop
        currentInteractable.OnDrop(this);

        // check if kinematic or physics based
        if (currentInteractable.GetComponent<Rigidbody>().isKinematic) {
            currentInteractable.transform.parent = null;
        }
        else {
            // Apply velocity
            Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
            targetBody.velocity = pose.GetVelocity();
            targetBody.angularVelocity = pose.GetAngularVelocity();

            // Detach
            joint.connectedBody = null;
        }

        // Clear active hand
        currentInteractable.activeHand = null;
        currentInteractable = null;
    }

    private MyInteractable GetNearestInteractable() {
        MyInteractable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (MyInteractable interactable in inRangeInteractables) {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance) {
                minDistance = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }
    private void OnTriggerEnter(Collider collider) {
        if (!collider.gameObject.CompareTag("Interactable"))
            return;

        inRangeInteractables.Add(collider.gameObject.GetComponent<MyInteractable>());
    }

    private void OnTriggerExit(Collider collider) {
        if (!collider.gameObject.CompareTag("Interactable"))
            return;
        inRangeInteractables.Remove(collider.gameObject.GetComponent<MyInteractable>());
    }
}
