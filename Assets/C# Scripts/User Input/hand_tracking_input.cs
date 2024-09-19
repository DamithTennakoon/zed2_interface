// Goal: Track the wrist position of each of the hand models and debug to console.
// TO-DO: Inherit joint transform for the hand_joint_render.cs class <- Current system is redundent

using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class hand_tracking_input : MonoBehaviour
{
    // Define Transform object to store the left and right hand wrist
    public Transform rightWrist;
    public Transform leftWritst;

    // Define Transform objects to store key joints for gestures 
    public Transform leftThumbJoint4; // Refer to "Hand Tracking Joints" diagram on documention for joint numbering system
    public Transform leftIndexJoint8;
    public Transform leftPalmJoint0;
    public Transform leftRingJoint16;
    public Transform rightThumbJoint4;
    public Transform rightIndexJoint8;


    // Define a empty array of length 6 with float values (Float32)
    public float[] handTrackingData = new float[6];


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Store the position of each transform into Hand Tracking Data array
        handTrackingData[0] = rightWrist.position.x;
        handTrackingData[1] = rightWrist.position.y;
        handTrackingData[2] = rightWrist.position.z;
        handTrackingData[3] = leftWritst.position.x;
        handTrackingData[4] = leftWritst.position.y;
        handTrackingData[5] = leftWritst.position.z;

    }
}
