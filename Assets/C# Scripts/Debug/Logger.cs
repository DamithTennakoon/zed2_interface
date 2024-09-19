using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    // Inherit the keyboard_input class and create an instance of it
    public keyboard_input keyboardInput;

    // Construct and instance of the hand_tracking class
    public hand_tracking_input handTrackingInput;

    // Reference data_handler class
    public data_handler dataHandler;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug the data from the Keyboard Input class
        //Debug.Log(keyboardInput.keydown);

        // Debug the data from the Hand Tracking Input class
        Debug.Log(handTrackingInput.handTrackingData[0].ToString() + ',' + handTrackingInput.handTrackingData[1].ToString() + "," + handTrackingInput.handTrackingData[2].ToString());
        //Debug.Log("NORM: " + dataHandler.convertHandTrackedData(handTrackingInput.handTrackingData));
    }
}
