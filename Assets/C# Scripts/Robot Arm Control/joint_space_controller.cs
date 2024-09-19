// Objective: Set the joint angles of the myCobot 320 M5 model's transforms, in real-time.
// Depenediencies: udp_client.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joint_space_controller : MonoBehaviour
{
    // Create an instance of the udp_client class -> access to RX joint angles
    [SerializeField] private udp_client udpClient;

    // Create an instance of the data_handler class -> access the data conversion methods
    [SerializeField] private data_handler dataHandler;

    // Construct a class-level local float array of length 6 to store the converted robot joint angles -> string array to float array
    public float[] robotJointAnglesArray = new float[0];

    // Construct an array of 6-float values
    public Transform[] jointTransforms = new Transform[6];

    void Start()
    {
        // Initialize the robot joint angles array to 0-degrees 
        for (int i = 0; i < robotJointAnglesArray.Length; i++)
        {
            robotJointAnglesArray[i] = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Convert received string data of robot joint angles into a float data
        robotJointAnglesArray = dataHandler.convertRobotJointAngleData(udpClient.messageRX);

        // Rotate each of the 6 joints
        // Joint 1 - Rotation around Y-AXIS
        jointTransforms[0].localEulerAngles = new Vector3(0f, robotJointAnglesArray[0], 0f);

        // Joint 2 - Rotation around X-AXIS
        jointTransforms[1].localEulerAngles = new Vector3(-1 * robotJointAnglesArray[1], 0f, 0f);

        // Joint 3 - Rotation around X-AXIS
        jointTransforms[2].localEulerAngles = new Vector3(-1 * robotJointAnglesArray[2], 0f, 0f);

        // Joint 4 - Rotation around X-AXIS
        jointTransforms[3].localEulerAngles = new Vector3(-1 * robotJointAnglesArray[3], 0f, 0f);

        // Joint 5 - Rotation around Y-AXIS
        jointTransforms[4].localEulerAngles = new Vector3(0f, robotJointAnglesArray[4], 0f);

        // Joint 6 - Rotation around Y-AXIS
        jointTransforms[5].localEulerAngles = new Vector3(robotJointAnglesArray[5], 0f, 0f);

    }
}
