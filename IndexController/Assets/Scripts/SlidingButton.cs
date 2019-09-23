using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingButton : MonoBehaviour {
    public Transform invisableButtonPart;
    public float maxHeight;
    public float maxDepth;

    private void Update() {
        if (invisableButtonPart.transform.localPosition.y > maxHeight)
            transform.localPosition = new Vector3(0, maxHeight, 0);
        else if (invisableButtonPart.transform.localPosition.y < maxDepth)
            transform.localPosition = new Vector3(0, maxDepth, 0);
        else
            transform.localPosition = new Vector3(0, invisableButtonPart.transform.localPosition.y, 0);
    }
}
