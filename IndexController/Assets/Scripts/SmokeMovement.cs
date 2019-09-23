using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeMovement : MonoBehaviour {
    private Vector3 randomPos;
    // the lower the value the faster it is
    private float shrinkSpeed = 60;
    void Start() {
        randomPos = transform.position + UnityEngine.Random.insideUnitSphere * 5f;
        float random = Random.Range(0.1f, 0.3f);
        transform.localScale = new Vector3(random, random, random);
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, randomPos, 0.025f);
        transform.localScale = new Vector3(transform.localScale.x - transform.localScale.x / shrinkSpeed, transform.localScale.y - transform.localScale.y / shrinkSpeed, transform.localScale.z - transform.localScale.z / shrinkSpeed);
    }
}
