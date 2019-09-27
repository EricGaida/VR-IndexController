using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRocketsBack : TriggerButton {
    public Transform rocket1;
    public Transform rocket2;

    public override void Action() {
        rocket1.position = new Vector3(2.5f, 1.25f, 2.75f);
        rocket1.rotation = Quaternion.Euler(Vector3.zero);
        rocket1.GetComponent<Rigidbody>().velocity = Vector3.zero;
        rocket1.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        rocket2.position = new Vector3(2f, 1.25f, 2.75f);
        rocket2.rotation = Quaternion.Euler(Vector3.zero);
        rocket2.GetComponent<Rigidbody>().velocity = Vector3.zero;
        rocket2.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
