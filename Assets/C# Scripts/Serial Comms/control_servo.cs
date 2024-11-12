// Objective: Write serial data, for specified port @baud rate, to control a Servo motor on an Arduino board.
// Written by: Damith Tennakoon
// Dependencies: <>,

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports;
using System.Threading;

public class control_servo : MonoBehaviour
{
    // Serial port for Arduino
    private SerialPort arduinoPort;

    // Serial communication settings
    public string portName = "COM6";
    public int baudRate = 9600;
    private bool isRunning = true;

    // Thread for serial communication
    private Thread serialThread;

    // Unity object to track
    [SerializeField] private Transform targetObject;
    public int outputAngle = 0;

    // Shared rotation variable
    private float yRotation;
    private readonly object rotationLock = new object();

    void Start()
    {
        // Initialize and open serial port
        arduinoPort = new SerialPort(portName, baudRate);

        try
        {
            if (!arduinoPort.IsOpen)
            {
                arduinoPort.Open();
                Debug.Log("Serial port opened successfully.");
            }
            // Start serial write thread
            serialThread = new Thread(SerialWrite);
            serialThread.Start();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Could not open serial port: " + e.Message);
            isRunning = false;
        }
    }

    void Update()
    {
        // Update yRotation from the main thread
        lock (rotationLock)
        {
            yRotation = targetObject.localEulerAngles.y;

            // Counter Euler rotation coversion range
            if (yRotation > 180)
            {
                yRotation -= 360;
            }
        }
    }

    void SerialWrite()
    {
        Debug.Log("SerialWrite thread started.");
        while (isRunning)
        {
            int rotationInt;

            // Safely access yRotation
            lock (rotationLock)
            {
                rotationInt = 90 - (Mathf.RoundToInt(yRotation));
                outputAngle = rotationInt;
            }

            // Write to Arduino if the port is open
            try
            {
                if (arduinoPort != null && arduinoPort.IsOpen)
                {
                    arduinoPort.WriteLine(rotationInt.ToString());
                    Debug.Log("Sending rotation: " + rotationInt); // Debug log
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error in SerialWrite: " + e.Message);
            }

            Thread.Sleep(100); // Control the frequency of updates
        }
    }

    private void OnApplicationQuit()
    {
        // Stop the serial communication thread and close the port
        isRunning = false;
        if (serialThread != null && serialThread.IsAlive)
        {
            serialThread.Join();
        }
        if (arduinoPort != null && arduinoPort.IsOpen)
        {
            arduinoPort.Close();
        }
    }
}


