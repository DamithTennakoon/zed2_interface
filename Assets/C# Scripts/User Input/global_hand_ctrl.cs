/* ----------------------------------------------------------
 * Class Name: global_hand_ctrl
 * Objective: Detects three types of hand destures to control position, orientation, and both in the global robot scene.
 * Written by: Damith Tennakoon
 * Date: 2025-05-07
 * -----------------------------------------------------------
 */

// Default libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global_hand_ctrl : MonoBehaviour
{
    // Define user parameters
    [Header("Right Hand Transforms")]
    [SerializeField] private Transform R_WRIST;
    [SerializeField] private Transform R_THUMB_TIP;
    [SerializeField] private Transform R_INDEX_FINGER_TIP;
    [SerializeField] private Transform R_MIDDLE_FINGER_TIP;
    [SerializeField] private Transform R_RING_FINGER_TIP;
    [SerializeField] private Transform R_PINKY_TIP;

    // Define gesture threshold distances;
    [Header("Gesture Thresholds")]
    [SerializeField] private float R_PINCH_THRESH = 0.012f;
    [SerializeField] private float R_TRI_PINCH_THRESH = 0.012f;
    [SerializeField] private float R_GRIP_THRESH = 0.09f;

    // Define internal parameters
    [Header("Gestection Detection States")]
    public bool RIGHT_PINCH = false;
    public bool RIGHT_PINCH_GRIP = false;
    public bool RIGHT_GRIP = false;

    private void Update()
    {
        // Check if all joint transforms are valid
        if (!(R_WRIST && R_THUMB_TIP && R_INDEX_FINGER_TIP && R_MIDDLE_FINGER_TIP && R_RING_FINGER_TIP && R_PINKY_TIP))
        {
            Debug.LogWarning("[global_hand_ctrl]: One of more transform objects hand joints were not initialized in the inspector");
            return;
        }

        // Compute distances
        float R_DIST_4_8 = Vector3.Distance(R_THUMB_TIP.position, R_INDEX_FINGER_TIP.position);
        float R_DIST_4_12 = Vector3.Distance(R_THUMB_TIP.position, R_MIDDLE_FINGER_TIP.position);
        float R_DIST_8_0 = Vector3.Distance(R_INDEX_FINGER_TIP.position, R_WRIST.position);
        float R_DIST_12_0 = Vector3.Distance(R_MIDDLE_FINGER_TIP.position, R_WRIST.position);
        float R_DIST_16_0 = Vector3.Distance(R_RING_FINGER_TIP.position, R_WRIST.position);
        float R_DIST_20_0 = Vector3.Distance(R_PINKY_TIP.position, R_WRIST.position);

        // Right hand pinch-grip gesture
        if ((R_DIST_4_8 < R_PINCH_THRESH) && (R_DIST_12_0 < R_GRIP_THRESH) && (R_DIST_16_0 < R_GRIP_THRESH) && (R_DIST_20_0 < R_GRIP_THRESH))
        {
            RIGHT_PINCH_GRIP = true;
            return;
        }
        else
        {
            RIGHT_PINCH_GRIP = false;
        }

        // Right hand pinch gesture
        RIGHT_PINCH = R_DIST_4_8 < R_PINCH_THRESH;

        // Right hand grip gesture
        if ((R_DIST_8_0 < R_GRIP_THRESH) && (R_DIST_12_0 < R_GRIP_THRESH) && (R_DIST_16_0 < R_GRIP_THRESH) && (R_DIST_20_0 < R_GRIP_THRESH))
        {
            RIGHT_GRIP = true;
        }
        else
        {
            RIGHT_GRIP = false;
        }

    }

}   


