// Goal: Parse through data collected from various input systems into a transmittable and known format for descryption

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data_handler : MonoBehaviour
{
    // Define data codes
    public string htcDataCode = "HTC";

    // Define method to handle the hand tracking data and convert to a transmitting String message
    // OUT FORMAT: "X.XXX, X.XXX, X,XXX ...."
    public string convertHandTrackedData(float[] values)
    {
        string result = htcDataCode + "," + string.Join(",", values);
        return result;
    }

    // Define method to handle the received robot joint angle data into a float array of length 6
    // OUT FORMAT: [X.Xf, X.Xf, X.Xf, X.Xf, X.Xf, X.Xf]
    public float[] convertRobotJointAngleData(string messageRX)
    {
        // Convert input String into a array of Strings through splitting using a csv format
        string[] rxMsgStringArray = messageRX.Split(',');

        // Define a float array of length 6 -> implicitly define the length
        float[] jointAnglesArray = new float[rxMsgStringArray.Length];

        // Convert each string value into a floating point value and populate the float array
        for (int i = 0; i < rxMsgStringArray.Length; i++)
        {
            // Cast string to float
            jointAnglesArray[i] = float.Parse(rxMsgStringArray[i].Trim());
        }

        // Return the resulting robot joint angles array
        return jointAnglesArray;
    }
}
