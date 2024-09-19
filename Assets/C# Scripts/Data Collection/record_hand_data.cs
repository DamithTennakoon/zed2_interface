// Objective: Write the coordinates of the left and right hand joints into .CSV file, with labels (training data for neural network)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Import libraries
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class record_hand_data : MonoBehaviour
{
    // Define Transform array of length 21 for the right hand joints
    [SerializeField] private Transform[] rightHandJointTransforms = new Transform[21];

    // Define Transform array of length 20 to store the relative positions of each joint
    public Vector3[] rightHandJointRelativeTransforms = new Vector3[20];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Define the name of the .txt file
        string fileName = "rightHandOpenData.txt";

        // Compute relative positon Transforms
        computeRelativePosition(rightHandJointTransforms, rightHandJointRelativeTransforms);

        // Collect data
        addRecord(fileName);

    }

    // Creates and writes a CSV file of the Transform objects' Vector3 positions (XYZ)
    public void addRecord(string filePath)
    {
        try
        {
            // Create an instance of the StreamWriter class
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filePath, true))
            {
                // Write a new line

                // Iterate through each joint and write store coordinate
                for (int i = 0; i < rightHandJointRelativeTransforms.Length; i++)
                {
                    // Single input writer
                    file.Write(rightHandJointRelativeTransforms[i][0].ToString() + ",");
                    file.Write(rightHandJointRelativeTransforms[i][1].ToString() + ",");
                    file.Write(rightHandJointRelativeTransforms[i][2].ToString() + ",");
                }

                // Write the label of the gesture
                file.WriteLine("open");

            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Unable to write to the .txt file :", ex);
        }

    }

    // Computes the relative position vectors of each joint to the palm joint
    public void computeRelativePosition(Transform[] jointData, Vector3[] relativeTransformData)
    {
        // Extract parent joint positions
        float targetX = jointData[0].position.x;
        float targetY = jointData[0].position.y;
        float targetZ = jointData[0].position.z;

        // Iterate through each joint and compute relative positions
        for (int i = 1; i < jointData.Length; i++)
        {
            // Compute difference
            Vector3 relPosistion = new Vector3(targetX - jointData[i].position.x, targetY - jointData[i].position.y, targetZ - jointData[i].position.z);

            // Set position vector to transform list
            relativeTransformData[i - 1] = relPosistion;
        }
    }

}
