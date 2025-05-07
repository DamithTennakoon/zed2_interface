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
    // Define user parameters
    [Header("Robot Model Setup")]
    public Transform EndEffectorTransorm;
    public GameObject Target;
    [SerializeField] private Transform RootComponent;
    public global_hand_ctrl GlobalHandCtrl;

    // Define internal parameters
    [Header("Target Pose and States")]
    public Vector3 TargetEndEffectorPos;
    public Quaternion TargetEndEffectorRot;
    public Vector3 CurrentEndEffectorPos;
    public bool IsControlActive = false; // Set to true when the this script is executed

    // Define user input state
    [Header("Control Type States")]
    [SerializeField] private bool ComputerBasedControl = false;

    private void Start()
    {
        // Initialize components
        TargetEndEffectorPos = Target.transform.position;
        TargetEndEffectorRot = Target.transform.rotation;

    }

    void Update()
    {
        // Validate components
        if (!(Target && EndEffectorTransorm && GlobalHandCtrl))
        {
            Debug.LogWarning("[global_robot_ctrl]: One or more objects not been initialized");
            return;
        }

        if (!(ComputerBasedControl))
        {
            // Compute only position control
            if (GlobalHandCtrl.RIGHT_PINCH)
            {
                TargetEndEffectorPos = Target.transform.position - RootComponent.position; // Compute and update the target position (robot_base_coord)
                IsControlActive = true; // Set control state to high
                return;
            }
            else
            {
                IsControlActive = false;
            }

            // Compute only orientation control
            if (GlobalHandCtrl.RIGHT_PINCH_GRIP)
            {
                TargetEndEffectorRot = Target.transform.rotation; // Compute and update the target orientation
                IsControlActive = true;
                return;
            }
            else
            {
                IsControlActive = false;
            }

            // Compute position & orientation control
            if (GlobalHandCtrl.RIGHT_GRIP)
            {
                TargetEndEffectorPos = Target.transform.position - RootComponent.position;
                TargetEndEffectorRot = Target.transform.rotation;
                IsControlActive = true;
                return;
            }
            else
            {
                IsControlActive = false;
            }
        }
        else
        {
            if (EndEffectorTransorm && Target && GlobalHandCtrl)
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
}
