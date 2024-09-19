// Objective: Provide visuals for live meta data in the MR HRI interface
// Dependencies: hand_tracking_input.cs, TMPro

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class meta_text_data : MonoBehaviour
{
    // Create a TextMeshPro object to be initialize in the INSPECTOR
    [SerializeField] private TextMeshProUGUI leftHandPinchText;

    // Create an instance of the hand_tracking_input class
    //[SerializeField] private hand_tracking_input handTrackingInput; // DEPRECATED
    [SerializeField] private gesture_recognition gestureRecognition;

    void Start()
    {
        // Initialize left hand pinch gesture state to be NONE:
        leftHandPinchText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // Change the text of the left hand pinch text based on pinch gesture state
        if (gestureRecognition.leftHandPinchState == true)
        {
            leftHandPinchText.text = "L-PINCH: TRUE";
        }
        else 
        {
            leftHandPinchText.text = "L-PINCH: FALSE";
        }
    }
}
