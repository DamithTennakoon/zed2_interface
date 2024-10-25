// Objetive: Compute the normalized vector for ominidirectional control & instance of gripper control.
// Dependencies: <joint_storage.cs>
// Author: Damith Tennakoon
// NOTE: AS A PART OF HRI VERSION II (SECOND YEAR OF MASTER'S)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compute_hand_control_v2 : MonoBehaviour
{
    // Create an instance of the joint_storage class
    [SerializeField] private joint_storage jointStorage;

    // Store joint-joint distance values
    [SerializeField] private float dist4_8_rh = 0.0f;
    [SerializeField] private float dist8_0_rh = 0.0f;
    [SerializeField] private float dist12_0_rh = 0.0f;
    [SerializeField] private float dist16_0_rh = 0.0f;
    [SerializeField] private float dist20_0_rh = 0.0f;

    // Define GameObjects and Transforms
    [SerializeField] private Transform anchorTransform;
    [SerializeField] private GameObject motionFlagSphere;
    [SerializeField] private GameObject gripperFlagSphere;

    // Define materials
    [SerializeField] private Material greenMat;
    [SerializeField] private Material redMat;
    [SerializeField] private Material blueMat;
    [SerializeField] private Material purpleMat;

    // Define hand gesture thresholds
    private float dist4_8_rh_pos_thresh = 0.012f;
    private float dist12_0_rh_pos_thresh = 0.09f;
    private float dist16_0_rh_pos_thresh = 0.09f;
    private float dist20_0_rh_pos_thresh = 0.09f;
    private float dist8_0_rh_grip_thresh = 0.09f;
    private float dist12_0_rh_grip_thresh = 0.09f;
    private float dist16_0_rh_grip_thresh = 0.09f;
    private float dist20_0_rh_grip_thresh = 0.09f;

    // Define variables for calculations
    public float[] handControlData = new float[7]; // Transmission data array to ROS server
    private int gripCount = 0; // Number of pinches detected
    private float gripTimer = 0.0f; // Timer to detect consecutive pinches (countdown begins)
    private float gripWindow = 1.0f; // Fixed time window to register multiple pinches
    private bool isGripping = false; // Flag to check if pinching is occuring

    void Start()
    {
        // Initialize values of each index to 0f to ensure robot arm safety
        for (int i = 0; i < handControlData.Length; i++)
        {
            handControlData[i] = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Compute the distances to joint combinations
        dist4_8_rh = Vector3.Distance(jointStorage.rightHandJoints[4].position, jointStorage.rightHandJoints[8].position);
        dist8_0_rh = Vector3.Distance(jointStorage.rightHandJoints[8].position, jointStorage.rightHandJoints[0].position);
        dist12_0_rh = Vector3.Distance(jointStorage.rightHandJoints[12].position, jointStorage.rightHandJoints[0].position);
        dist16_0_rh = Vector3.Distance(jointStorage.rightHandJoints[16].position, jointStorage.rightHandJoints[0].position);
        dist20_0_rh = Vector3.Distance(jointStorage.rightHandJoints[20].position, jointStorage.rightHandJoints[0].position);

        // Compute motion gesture detection
        if (dist4_8_rh < dist4_8_rh_pos_thresh) // When pinch
        {
            motionFlagSphere.GetComponent<MeshRenderer>().material = blueMat;

            if ((dist12_0_rh < dist12_0_rh_pos_thresh) && (dist16_0_rh < dist16_0_rh_pos_thresh) && (dist20_0_rh < dist20_0_rh_pos_thresh)) // When grip with pinch
            {
                Vector3 directionVector = Vector3.Normalize(jointStorage.rightHandJoints[8].position - anchorTransform.position); // Compute the normalized direction vector

                for (int i = 0; i < 3; i++)
                {
                    handControlData[i] = directionVector[i]; // Set the direction vector to the global hand control data for transmission (settting raw data -> Coordinate transformations handled at ROS node)
                }

                motionFlagSphere.GetComponent<MeshRenderer>().material = greenMat;
            }
            else // When pinch but no grip
            {
                for (int i = 0; i < 3; i++)
                {
                    handControlData[i] = 0.0f; // Zero the direction vector
                }
            }
        }
        else // When no pinch and no grip
        {
            motionFlagSphere.GetComponent<MeshRenderer>().material = redMat;

            for (int i = 0; i < 3; i++)
            {
                handControlData[i] = 0.0f; // Zero the direction vector
            }
        }

        // Detect gripper gesture
        DetectGrip();
        CountGrips();
    }

    // Function: Detects when a gripper gesture has been invoked
    void DetectGrip()
    {
        // Check whether the thresholds have been satisfied
        if ((dist8_0_rh < dist8_0_rh_grip_thresh) && (dist12_0_rh < dist12_0_rh_grip_thresh) && (dist16_0_rh < dist16_0_rh_grip_thresh) && (dist20_0_rh < dist20_0_rh_grip_thresh))
        {
            if (isGripping == false) // Ensure grip count is not incremented when gesture is held without release
            {
                isGripping = true;
                gripCount++;
                gripTimer = gripWindow; // Reset the timer when a pinch has been recognized
            }
        }
        else
        {
            isGripping = false; // Thresholds were not met
        }
    }

    // Function: Count the number of pinches to classify the gripper state
    void CountGrips()
    {
        // Ensure gripper timer is non-negative
        if (gripTimer > 0)
        {
            gripTimer -= Time.deltaTime; // Count down the timer

            // When the timer runs out, process the collected counts
            if (gripTimer <= 0)
            {
                if (gripCount == 2) // Open gripper state
                {
                    gripperFlagSphere.GetComponent<MeshRenderer>().material = purpleMat;
                }
                else if (gripCount == 3) // Close gripper state
                {
                    gripperFlagSphere.GetComponent<MeshRenderer>().material = redMat;
                }

                gripCount = 0; // Reset the grip count and time for next detection window
            }
        }
    }
}