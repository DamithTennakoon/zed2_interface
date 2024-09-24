// Objective: Record the distances between specific joints.
// Dependencies: <joint_storage.cs>
// NOTE: AS A PART OF HRI VERSION II (SECOND YEAR OF MASTER'S)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Import libraries
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class collect_joint_distances : MonoBehaviour
{
    // Create instance of the joint_storage class
    [SerializeField] private joint_storage jointStorage;

    // Store a counter for the elapsed time
    private float elapsedTime = 0.0f;

    // Store distance values
    [SerializeField] private float dist4_8_rh = 0.0f;
    [SerializeField] private float dist12_0_rh = 0.0f;
    [SerializeField] private float dist16_0_rh = 0.0f;
    [SerializeField] private float dist20_0_rh = 0.0f;

    // Define the name of the .txt file
    private string fileName = "TESTING_TESTING.txt";

    void Start()
    {
        // Initialize the header of the .CSV file
        using (StreamWriter writer = new StreamWriter(fileName, true))
        {
            writer.WriteLine("Time," +
                "Dist4_8," +
                "Dist12_0,Dist16_0,Dist20_0");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Compute the distances to joint combinations
        dist4_8_rh = Vector3.Distance(jointStorage.rightHandJoints[4].position, jointStorage.rightHandJoints[8].position);

        dist12_0_rh = Vector3.Distance(jointStorage.rightHandJoints[12].position, jointStorage.rightHandJoints[0].position);
        dist16_0_rh = Vector3.Distance(jointStorage.rightHandJoints[16].position, jointStorage.rightHandJoints[0].position);
        dist20_0_rh = Vector3.Distance(jointStorage.rightHandJoints[20].position, jointStorage.rightHandJoints[0].position);

        // Record data
        if (dist4_8_rh > 0.0)
        {
            addRecord(fileName);
        }

        // Update time
        elapsedTime += Time.deltaTime;
    }

    private void addRecord(string filePath)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filePath, true))
            {
                // Write values to file
                file.Write(elapsedTime.ToString() + ",");

                file.Write(dist4_8_rh.ToString() + ",");

                file.Write(dist12_0_rh.ToString() + ",");
                file.Write(dist16_0_rh.ToString() + ",");
                file.WriteLine(dist20_0_rh.ToString());
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("UNABLE TO WRITE TO THE .TXT FILE: ", ex);
        }
    }

}
