// Objective: Create a UDP client architecture that transmits hand joint data to a UDP client using a NN model for classification.
// Dependencies: <record_hand_data.cs>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Required Imports for system
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.Threading.Tasks; // Import System.Threading.Tasks for async operations

public class nn_validation : MonoBehaviour
{
    // Define server IP and socket number
    [SerializeField] private string serverIP = "130.63.221.229";
    [SerializeField] private int serverPort = 2222;

    // Define string object to store message to transmit 
    public string messageTX = "INITIALIZE COMMS";

    // Create UDP cliebt object and IPEndPoint object
    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint;

    // Inherit the record_hand_data class and create an instance of it
    public record_hand_data recordHandData;

    void Start()
    {
        // Create a new instance of the UDP client class
        udpClient = new UdpClient();

        // Define and IP adress object and attach the server ip string object to it, bind the IP Adress object and the socket number to create an instance of an IP End Point object
        IPAddress ipAdress = IPAddress.Parse(serverIP);
        remoteEndPoint = new IPEndPoint(ipAdress, serverPort);

        // Begin asynchronous communication loops
        _ = CommunicationLoopAsync();

    }

    // Define method to transmit a message to the UDP server asynchronously
    async Task CommunicationLoopAsync()
    {
        try
        {
            while (true)
            {
                // Convert the Right Hand Joint Transform Vector3 into a single csv string object
                messageTX = Vector3toString(recordHandData.rightHandJointRelativeTransforms);

                // Convert transmit message into bytes through UTF-8 encoding
                byte[] dataTX = Encoding.UTF8.GetBytes(messageTX);

                // Transmit bytes Asynchrounously
                await udpClient.SendAsync(dataTX, dataTX.Length, remoteEndPoint);

                // Invoke a delay between tranmission of next message
                await Task.Delay(TimeSpan.FromSeconds(0.01f));
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error in data transmission: " + e.Message);
        }
    }

    // Define a method to convert the Vector3 array into a csv string
    private string Vector3toString(Vector3[] inputVector3Array)
    {
        // Define string object
        StringBuilder outputString = new StringBuilder();

        // Iterate through Vector3 array and attach each component to StringBuilding with csv
        foreach (var vector in inputVector3Array)
        {
            outputString.AppendFormat("{0}, {1}, {2},", vector.x, vector.y, vector.z);
        }

        // Remove the last comma
        if (outputString.Length > 0)
        {
            outputString.Length--; // Removes last character
        }

        // Return string 
        return outputString.ToString();
    }

    // Define method to close the UDP client system safely when program stops
    private void OnDestroy()
    {
        // Close UDP client when UdpClient object is destroyed
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }
}
