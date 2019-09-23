using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Hand"))
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 4f;

    }
}
