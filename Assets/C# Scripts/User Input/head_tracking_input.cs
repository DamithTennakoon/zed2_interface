// Objective: Retrieve the pitch and yaw angles of the HMD and inject it to the udp_client class.
// Dependencies: <udp_client.cs,>
// Author: Damith Tennakoon
// NOTE: AS A PART OF HRI VERSION II (SECOND YEAR OF MASTER'S)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class head_tracking_input : MonoBehaviour
{
    // Define external references to classes
    [SerializeField] private compute_hand_control_v2 computeHandControlV2;
    [SerializeField] private Transform hmdTransform;

    // Define variables used for calculations
    public float hmdPitch = 0.0f;
    public float hmdYaw = 0.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Angle corrections
        if (hmdTransform.localEulerAngles.x > 180f)
        {
            hmdPitch = (-1)*(hmdTransform.localEulerAngles.x - 360);
        }
        else
        {
            hmdPitch = (-1)*hmdTransform.localEulerAngles.x;
        }

        if (hmdTransform.localEulerAngles.y > 180f)
        {
            hmdYaw = hmdTransform.localEulerAngles.y - 360;
        }
        else
        {
            hmdYaw = hmdTransform.localEulerAngles.y;
        }

        // Inject head tracking data to Hand Control Data object
        if (computeHandControlV2.handControlData != null)
        {
            computeHandControlV2.handControlData[7] = hmdPitch;
            computeHandControlV2.handControlData[8] = hmdYaw;
        }

        
    }
}
