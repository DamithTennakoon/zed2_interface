// Objective: Control the up-axis orientation of the "Camera Controller" utilizing a right-fist gesture.
// Dependencies: <global_hand_gestures.cs>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global_camera_controller : MonoBehaviour
{
    // Construct an instance of the global_hand_gestures class -> access the right-first boolean state
    [SerializeField] private global_hand_gestures globalHandGestures;

    // Create a Transform object to store the pose of the right hand gizmo joint -> represent the current orientation of the right hand
    [SerializeField] private Transform rightHandPose;

    // Create an transform object to store the refernce frame in global coordinates
    [SerializeField] private Transform referencePose;

    // Create a float object to store the orientation of the right hand
    [SerializeField] private float rightHandEulerAngle;

    // Create a Vector3 object to store the difference in poses
    [SerializeField] private Vector3 diffVector = new Vector3(0f, 0f, 0f);

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Compute the difference between the right hand pose transform and the reference pose transform in global coordinates
        diffVector = referencePose.position - rightHandPose.position;
        rightHandEulerAngle = Vector3.Magnitude(diffVector);

        // 
    }
}
