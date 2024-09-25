// Objetive: Compute the normalized vector for ominidirectional control.
// Dependenvies: <joint_storage.cs>
// Author: Damith Tennakoon
// NOTE: AS A PART OF HRI VERSION II (SECOND YEAR OF MASTER'S)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compute_hand_control_v2 : MonoBehaviour
{
    // Create an instance of the joint_storage class
    [SerializeField] private joint_storage jointStorage;

    // Store distance values
    [SerializeField] private float dist4_8_rh = 0.0f;
    [SerializeField] private float dist12_0_rh = 0.0f;
    [SerializeField] private float dist16_0_rh = 0.0f;
    [SerializeField] private float dist20_0_rh = 0.0f;

    // Define GameObjects and Transforms
    [SerializeField] private Transform anchorTransform;
    [SerializeField] private GameObject flagSphere;

    // Define materials
    [SerializeField] private Material greenMat;
    [SerializeField] private Material redMat;
    [SerializeField] private Material blueMat;

    // Define hand gesture thresholds
    private float dist4_8_rh_thresh = 0.012f;
    private float dist12_0_rh_thresh = 0.09f;
    private float dist16_0_rh_thresh = 0.09f;
    private float dist20_0_rh_thresh = 0.09f;

    // Define variables for calculations
    public float[] handControlData = new float[7];


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
        dist12_0_rh = Vector3.Distance(jointStorage.rightHandJoints[12].position, jointStorage.rightHandJoints[0].position);
        dist16_0_rh = Vector3.Distance(jointStorage.rightHandJoints[16].position, jointStorage.rightHandJoints[0].position);
        dist20_0_rh = Vector3.Distance(jointStorage.rightHandJoints[20].position, jointStorage.rightHandJoints[0].position);


        // Compute motion gesture detection
        if (dist4_8_rh < dist4_8_rh_thresh) // When pinch
        {
            flagSphere.GetComponent<MeshRenderer>().material = blueMat;

            if ((dist12_0_rh < dist12_0_rh_thresh) && (dist16_0_rh < dist16_0_rh_thresh) && (dist20_0_rh < dist20_0_rh_thresh)) // When grip with pinch
            {
                Vector3 directionVector = Vector3.Normalize(jointStorage.rightHandJoints[8].position - anchorTransform.position); // Compute the normalized direction vector

                for (int i = 0; i < 3; i++)
                {
                    handControlData[i] = directionVector[i]; // Set the direction vector to the global hand control data for transmission (settting raw data -> Coordinate transformations handled at ROS node)
                }

                flagSphere.GetComponent<MeshRenderer>().material = greenMat;
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
            flagSphere.GetComponent<MeshRenderer>().material = redMat;
        }
        
    }
}
