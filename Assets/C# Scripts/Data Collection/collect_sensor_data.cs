// Objective: Collect and store the sensor data of the ZEDM camera into a .CSV file.
// Dependencies: <>,

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using sl;

public class collect_sensor_data : MonoBehaviour
{
    // ZEDM objects to store sensory data
    public sl.Pose pose = new sl.Pose();
    [SerializeField] private ZEDManager zedManager;
    private ZEDCamera zedCamera;

    // Define user input fields
    public bool collectData = false;

    // Construct meta data objects 
    private float elapsedTime = 0.0f;
    private string displacementFilePath = "Assets/DisplacementData.csv";

    void Start()
    {
        // Initialize ZEDCamera object
        zedCamera = zedManager.zedCamera;

        // Initialize the header of the .CSV file
        using (StreamWriter writer = new StreamWriter(displacementFilePath, true))
        {
            writer.WriteLine("Time,Position");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Retrieve latest pose data
        zedCamera.GetPosition(ref pose, REFERENCE_FRAME.WORLD);

        // Compute data and store data into .csv file
        if (collectData)
        {
            // Store data
            float displacement = pose.translation.y * 1000; // convert from M to MM;

            // Write data
            using (StreamWriter writer = new StreamWriter(displacementFilePath, true))
            {
                writer.WriteLine(elapsedTime + "," + displacement);
            }

            // Debug the pose vector
            Debug.Log("[INFO] POSE: " + pose.translation);
        }

        // Update time counter
        elapsedTime += Time.deltaTime;
    }
}
