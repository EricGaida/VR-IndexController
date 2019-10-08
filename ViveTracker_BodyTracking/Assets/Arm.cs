using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {
    public Transform handAttachpoint;
    public Transform hipHandAttachPoint;

    private void Update() {
        handAttachpoint.transform.LookAt(hipHandAttachPoint);
        transform.position = Vector3.Lerp(handAttachpoint.position, hipHandAttachPoint.position, 0.5f);
        transform.localScale = new Vector3(0.1f, Vector3.Distance(handAttachpoint.position, hipHandAttachPoint.position), 0.1f);
    }
}
