using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour {
    public Transform hipAttachPoint;
    public Transform footAttachPoint;

    private void Update() {
        hipAttachPoint.transform.LookAt(footAttachPoint);
        transform.position = Vector3.Lerp(hipAttachPoint.position, footAttachPoint.position, 0.5f);
        transform.localScale = new Vector3(0.1f, Vector3.Distance(hipAttachPoint.position, footAttachPoint.position), 0.1f);
    }
}
