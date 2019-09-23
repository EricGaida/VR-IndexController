using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportButton : TriggerButton {
    public Transform player;
    public override void Action() {
        player.position = new Vector3(2.5f, 80.5f, -2.5f);
    }
}
