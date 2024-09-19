// Objective: Enable the operator to switch from FPV to Global views, and vice-versa, via hand gesture states.
// Dependencies: <hand_tracking_input.cs>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vision_systems_controller : MonoBehaviour
{
    // Construct an instance of the hand_tracking_input class -> access the left hand pinch gesture 
    //[SerializeField] private hand_tracking_input handTrackingInput; // MAJOR COMMENT: THE HAND_TRACKING_INPUT CLASS NO LONGER HAS A HANDTRACKINGINPUT VARIABLE(DEPRECATED) -> INSTEAD USE GESTURE_RECOGNITION CLASS
    [SerializeField] private gesture_recognition gestureRecognition;

    // Create GameObjects to attach in inspector -> store the ZEDStereo Rig, ZEDMono Rig, PCD, and Oculus Interaction Rig
    [SerializeField] private GameObject zedStereoRig;
    [SerializeField] private GameObject zedMonoRig;
    [SerializeField] private GameObject pcd;
    [SerializeField] private GameObject oculusInteractionRig;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the pinch gesture state -> when true, switch off the ZEDStereo Rig, switch on the ZEDMono Rig
        if (gestureRecognition.leftHandPinchState == true)
        {
            // Switch to Global visoin sytem
            //zedStereoRig.SetActive(false);
            //zedMonoRig.SetActive(true);
            //oculusInteractionRig.SetActive(true);
            //pcd.SetActive(true);

            // Load the Global View Scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            // Resume FPV view
            //pcd.SetActive(false);
            //zedMonoRig.SetActive(false);
            //zedStereoRig.SetActive(true);
            //oculusInteractionRig.SetActive(false);
        }

    }
}
