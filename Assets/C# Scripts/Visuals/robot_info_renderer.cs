// Objective: Render meta data of the robot arm's pose to the Canvas system.
// Dependencies: <joint_space_controller.cs>, <System>, <TMPro>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class robot_info_renderer : MonoBehaviour
{
    // Construct an instance of the joint_space_controller class -> access the robotJointAnglesArray object 
    [SerializeField] private joint_space_controller jointSpaceController;

    // Create a transform object -> Store end effector position
    [SerializeField] private Transform joint6Position;

    // Create TextMeshPro objects to attach the text objects in the inspector -> event-driven control
    [SerializeField] private TextMeshProUGUI joint1Text;
    [SerializeField] private TextMeshProUGUI joint2Text;
    [SerializeField] private TextMeshProUGUI joint3Text;
    [SerializeField] private TextMeshProUGUI joint4Text;
    [SerializeField] private TextMeshProUGUI joint5Text;
    [SerializeField] private TextMeshProUGUI joint6Text;

    [SerializeField] private TextMeshProUGUI altitudeText;

    void Update()
    {
        // Change the text of the joint angle information text
        joint1Text.text = "JOINT 1: " + jointSpaceController.robotJointAnglesArray[0].ToString() + "°";
        joint2Text.text = "JOINT 2: " + jointSpaceController.robotJointAnglesArray[1].ToString() + "°";
        joint3Text.text = "JOINT 3: " + jointSpaceController.robotJointAnglesArray[2].ToString() + "°";
        joint4Text.text = "JOINT 4: " + jointSpaceController.robotJointAnglesArray[3].ToString() + "°";
        joint5Text.text = "JOINT 5: " + jointSpaceController.robotJointAnglesArray[4].ToString() + "°";
        joint6Text.text = "JOINT 6: " + jointSpaceController.robotJointAnglesArray[5].ToString() + "°";

        altitudeText.text = "ALITITUDE: " + ((float)Math.Round(joint6Position.position.y, 2)).ToString() + "M";
    }
}
