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
    public Transform thumbTipJoint;
    public Transform indexTipJoint;

    // Store a counter for the elapsed time
    private float elapsedTime = 0.0f;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Define the name of the .txt file
        string fileName = "NonePinchGestureData.txt";

        // Output the relative distance
        float dist = Vector3.Distance(thumbTipJoint.position, indexTipJoint.position);
        Debug.Log("RELATIVE DISTANCE: " + dist);

        // Record data
        if (dist > 0.0)
        {
            addRecord(fileName, dist);
        }

        // Update time
        elapsedTime += Time.deltaTime;
    }

    private void addRecord(string filePath, float data)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filePath, true))
            {
                // Write the value
                file.WriteLine(data.ToString() + "," + elapsedTime.ToString());
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("UNABLE TO WRITE TO THE .TXT FILE: ", ex);
        }
    }
}
