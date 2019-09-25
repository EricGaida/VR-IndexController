using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLever : MonoBehaviour {
    public Transform target;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        rb.MovePosition(target.transform.position);
    }
}
