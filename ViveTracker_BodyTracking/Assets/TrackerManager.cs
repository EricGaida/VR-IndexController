using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TrackerManager : MonoBehaviour {
    public List<SteamVR_TrackedObject> viveTrackers;
    public int[] viveTrackersIndex;

    private void Awake() {
        int indexCount = 0;
        foreach (SteamVR_TrackedObject tracker in viveTrackers) {
            tracker.index = (SteamVR_TrackedObject.EIndex)viveTrackersIndex[indexCount++];
        }
    }

    private void Update() {
        int indexCount = 0;
        foreach (SteamVR_TrackedObject tracker in viveTrackers) {
            tracker.index = (SteamVR_TrackedObject.EIndex)viveTrackersIndex[indexCount++];
        }
    }
}
