/* ----------------------------------------------------------
 * Class Name: global_robot_materials
 * Objective: Detects the control types from the global_hand_ctrl class and changes material rendering properties.
 * Written by: Damith Tennakoon
 * Date: 2025-05-07
 * -----------------------------------------------------------
 */

// Default libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global_robot_materials : MonoBehaviour
{
    // Define user parameters
    [Header("Target Objects")]
    [SerializeField] private GameObject TargetCube;
    [SerializeField] private global_hand_ctrl GlobalHandCtrl;

    void Update()
    {
        // Validate object initializations
        if (!(TargetCube && GlobalHandCtrl))
        {
            Debug.LogWarning("[global_robot_materials]: One or more objects have not been initialized");
            return;
        }

        // Control render state of the target object
        if (GlobalHandCtrl.RIGHT_PINCH || GlobalHandCtrl.RIGHT_PINCH_GRIP || GlobalHandCtrl.RIGHT_GRIP)
        {
            TargetCube.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            TargetCube.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
