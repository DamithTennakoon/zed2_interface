using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Import libraries
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class compute_hand_control_v2 : MonoBehaviour
{
    // Store transforms of thumb and index finger joint tips
    public Transform joint0;
    public Transform joint4;
    public Transform joint8;
    public Transform joint12;
    public Transform joint16;
    public Transform joint20;

    // Store a counter for the elapsed time
    private float elapsedTime = 0.0f;

    // Store distance values
    private float dist4_8 = 0.0f;
    private float dist12_0 = 0.0f;
    private float dist16_0 = 0.0f;
    private float dist20_0 = 0.0f;

    // Define the name of the .txt file
    private string fileName = "OmniDirectionalCtrl.txt";

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
        dist4_8 = Vector3.Distance(joint4.position, joint8.position);

        dist12_0 = Vector3.Distance(joint12.position, joint0.position);
        dist16_0 = Vector3.Distance(joint16.position, joint0.position);
        dist20_0 = Vector3.Distance(joint20.position, joint0.position);

        // Record data
        if (dist4_8 > 0.0)
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

                file.Write(dist4_8.ToString() + ",");

                file.Write(dist12_0.ToString() + ",");
                file.Write(dist16_0.ToString() + ",");
                file.WriteLine(dist20_0.ToString());
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("UNABLE TO WRITE TO THE .TXT FILE: ", ex);
        }
    }
}
