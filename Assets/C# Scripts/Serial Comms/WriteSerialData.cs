using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// NOTE: Requires switch from .NET Standard 2.1 API to .NET Framework
using System.IO.Ports; 
using System.Threading;

public class WriteSerialData : MonoBehaviour
{
    // Create a thread to send and receive data to main game - won't break Unity
    Thread IOThread = new Thread(DataThread);
    // Instantiate Serial Port variable to store the serial port communcations
    private static SerialPort sp;

    // Instantiate variable to store outgoing message to the serial port
    private static string OutGoingMsg;

    // Instantiate variable to store an integer value for Servo position
    private int ServoPos = 0;

    // Added: Initialize transform to access ZED Cams orientation
    public Transform ZED_Orientation;

    // Added: Initialize float variable to store ZED Cams orientation
    public float ZED_Y_Angle;

    // Frunction: create function to read and write from serial port
    private static void DataThread() {
        // Initialize the Serial Port variable to corresponding USB port
        //string SerialPortPath = "/dev/cu.usbmodem143301";
        sp = new SerialPort("COM4", 115200);

        // Initialize OutGoingMsg as an empty string
        OutGoingMsg = "";

        // Open to serial port to for communication
        sp.Open();

        // Check if a message is stored in the Outgoing message variable
        // and send it out if its not EMPTY
        while (true) {
            // Check if the Outgoing message variable is empty
            if (OutGoingMsg != "") {

                // Write the Outgoing message to the serial port
                sp.Write(OutGoingMsg);

                // Reset the Outgoihg message to EMPTY so it does not repeat
                OutGoingMsg = "";
            }

            // Set the sleep time to match Arduino sleep time
            Thread.Sleep(10);
        }
    }

    // Function: Close the IO thread when the game stops so port becomes
    // available for other serial communcations
    private void OnDestroy()
    {
        // Close thread
        IOThread.Abort();

        // Close the serial port I/O
        sp.Close();
    }

    void Start()
    {
        // Begin the serial port I/O
        IOThread.Start();

        // Added: Initialize the angle value of the orientiation
        ZED_Y_Angle = ZED_Orientation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Change the value of the Outgoing message
        if (Input.GetKeyDown(KeyCode.Return))
        {

            OutGoingMsg = "1";
            //Debug.Log("90");
        }

        // Change the value of the Outgoing message
        else if (Input.GetKeyDown(KeyCode.Space))
        {

            OutGoingMsg = "0";
            //Debug.Log("0");
        }
        else {
            // Do nothing
        }

        // Added: Retrieve the angle value of the orientiation
        ZED_Y_Angle = ZED_Orientation.eulerAngles.y;
        // Added: Transmit 1/0 per ZED orientation
        if (ZED_Y_Angle > 90.0f) {
            OutGoingMsg = "1";
            Debug.Log("1");
        } 
        else {
            OutGoingMsg = "0";
            Debug.Log("0");
        }

    }
}
