using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
    public Transform headAttachPoint;
    public Transform hipAttachPoint;

    private void Update() {
        hipAttachPoint.transform.LookAt(headAttachPoint);
        transform.position = Vector3.Lerp(headAttachPoint.position, hipAttachPoint.position, 0.5f);
        transform.localScale = new Vector3(0.1f, Vector3.Distance(headAttachPoint.position, hipAttachPoint.position), 0.4f);
    }
}
