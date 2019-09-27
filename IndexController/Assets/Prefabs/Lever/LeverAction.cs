using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverAction : MonoBehaviour {

    private void Update() {
        float leverAngle = transform.localRotation.eulerAngles.x;
        if (leverAngle > 180)
            leverAngle += -360;
        // leverAngle is from -45 to 45
        Action(leverAngle);
    }

    public virtual void Action(float leverAngle) {
        Debug.Log("Action with the angle of: " + leverAngle);
        Debug.Log("Override this method with what the lever should do! \n Angle ist always between -45 and 45.");
    }
}
