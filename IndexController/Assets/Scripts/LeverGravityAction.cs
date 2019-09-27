using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeverGravityAction : LeverAction {
    public TextMeshProUGUI gravityDisplay;

    public override void Action(float leverAngle) {
        leverAngle += 45f;
        // change gravity
        if (leverAngle > 80f)
            Physics.gravity = new Vector3(0f, -9.8f, 0f);
        else if (leverAngle < 10f)
            Physics.gravity = new Vector3(0f, -1f, 0f);
        else
            Physics.gravity = new Vector3(0f, -9.8f, 0f) * (leverAngle / 100);

        // change UI
        gravityDisplay.text = "Gravity: " + System.Math.Round(Physics.gravity.y, 2);
    }
}
