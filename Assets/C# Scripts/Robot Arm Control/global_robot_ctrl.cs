/* ----------------------------------------------------------
 * Class Name: global_robot_ctrl
 * Objective: Uses a sphere object as the target position for the end effector an transmits this to the UDP_server for ROS handling.
 * Written by: Damith Tennakoon
 * Date: 2025-05-02
 * -----------------------------------------------------------
 */

// Default libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//

public class global_robot_ctrl : MonoBehaviour
{
    // Define user paramters
    public Transform EndEffectorTransorm;
    public GameObject Target;
    [SerializeField] private Transform RootComponent;
    public Vector3 TargetEndEffectorPos;
    public Quaternion TargetEndEffectorRot;
    public Vector3 CurrentEndEffectorPos;
    public bool IsControlActive = false; // Set to true when the this script is executed
    
    void Update()
    {
        // Compute the target location realtive to the base component, show a Vector3 for the current location of the end effector, and transmit
        if (EndEffectorTransorm && Target)
        {
            // Calculate and set
            TargetEndEffectorPos = Target.transform.position - RootComponent.position; // Computes the target position (robot_base_coord)
            CurrentEndEffectorPos.x = EndEffectorTransorm.position.x; // Computes the current position (robot_base_coord)
            CurrentEndEffectorPos.y = EndEffectorTransorm.position.y;
            CurrentEndEffectorPos.z = EndEffectorTransorm.position.z;

            // Get the quternion rotation of the target
            TargetEndEffectorRot = Target.transform.rotation;

            IsControlActive = true; // Set control state to high
        }
        else
        {
            // Log a warning and set the control state to low
            Debug.LogWarning("global_robot_ctrl: The end effector transform or target object is not attached");
            IsControlActive = false;
        }
    }
}
