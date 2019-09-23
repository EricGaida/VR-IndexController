using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitVelocity : MonoBehaviour {
    private Rigidbody player;

    private void Awake() {
        player = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (player.velocity.magnitude > 37) {
            player.AddForce(-player.velocity);
        }
    }
}
