// Goal: A UDP (User Datagram Protocol) client connecting to a server ip and port to transmit messages in real-time
// Dependencies: keyboard_input.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Required Imports for system
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.Threading.Tasks; // Import System.Threading.Tasks for async operations


public class udp_client : MonoBehaviour
{
    // Define server IP and socket number
    public string serverIP = "130.63.230.126";
    public int serverPort = 2222;

    // Define string object to store message to transmit
    public string messageTX = "INITIALIZE COMMS";

    // Define a string object to store received message
    public string messageRX = "INITIALIZE COMMS";

    // Create UDP client object and IPEndPoint object
    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint;

    // Inherit the keyboard_input class and create an instance of it
    public keyboard_input keyboardInput;

    // Construct and instance of the hand_tracking class
    public compute_hand_control computeHandControl;

    // Reference data_handler class
    public data_handler dataHandler;

    void Start()
    {
        // Create an instance of the UDP cleint
        udpClient = new UdpClient();

        // Define an IP Adress object and attach the server ip string object, bind the IP Adress object and the socket number to create a IP End Point object
        IPAddress ipAdress = IPAddress.Parse(serverIP);
        remoteEndPoint = new IPEndPoint(ipAdress, serverPort);

        // Begin the communication and receive loop asynchronously
        _ = CommuncationLoopAsync();
        _ = ReceiveLoopAsync();

    }

    // Define method to transmit message to UDP server asynchronously
    async Task CommuncationLoopAsync()
    {
        try
        {
            while (true)
            {
                // Priority input system: if there is no data coming from the keyboard, prioritze the hand tracking system as the input system, else (there is keyboard input), prioritize the keyboard input system
                if (keyboardInput.keydown == "INPUT SYSTEM 0")
                {
                    // Retrieve input data from Hand Tracking System 
                    messageTX = dataHandler.convertHandTrackedData(computeHandControl.handControlData);
                }
                else
                { 
                    // Retrieve input data from the Keyboard System
                    messageTX = keyboardInput.keydown; // Already a single string value 
                }

                // Convert tranmit message into bytes through UTF-8 encoding
                byte[] dataTX = Encoding.UTF8.GetBytes(messageTX);

                // Transmit bytes asynchornously
                await udpClient.SendAsync(dataTX, dataTX.Length, remoteEndPoint);

                // Time between tranmission of next message
                await Task.Delay(TimeSpan.FromSeconds(0.01f));
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error in data transmission: " + e.Message);
        }
    }

    // Define method to receive message from UDP server asynchronously 
    async Task ReceiveLoopAsync()
    {
        try
        {
            while (true)
            {
                // Receive bytes asynchronously
                UdpReceiveResult bytesRX = await udpClient.ReceiveAsync();

                // Convert RX bytes into string
                messageRX = Encoding.UTF8.GetString(bytesRX.Buffer);

                // TEMP -> Debug received data to console for validating
                Debug.Log("UDP CLIENT [DATA RX]: " + messageRX);

                // Time between reception of next message
                await Task.Delay(TimeSpan.FromSeconds(0.01f));
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error in data reception: " + e.Message);
        }
    }

    // Define method to close the UDP client system safely when program stops 
    private void OnDestroy()
    {
        // Close UDP client when UdpClient object is destroyed (when program is finished executing)
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }
}
