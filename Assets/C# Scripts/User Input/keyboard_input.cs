// Goal: Check for keyboard presses from user. Store and debug to console thee input keys.
// Depenedencies: <>
// Usages: <udp_server.cs>

/* NOTES:
- Arrow keys: Horizontal EE controls
- M/N: Vertical EE controls (down & up, respectively)
- Q/W: Gripper controls (open & close, respectively)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboard_input : MonoBehaviour
{
    // Define String object to store the name of the key press
    public string keydown = "INPUT SYSTEM 0";

    // Update is called once per frame
    void Update()
    {
        // Check for key "UpArrow"
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Store the name of the key, globally
            keydown = "UpArrow";
        }
        // Check for key "DownArrow"
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            keydown = "DownArrow";
        }
        // Check for key "RightArrow"
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            keydown = "RightArrow";
        }
        // Check for key "LeftArrow"
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            keydown = "LeftArrow";
        }
        // Check for key "N"
        else if (Input.GetKey(KeyCode.N))
        {
            keydown = "N";
        }
        // Check for key "M"
        else if (Input.GetKey(KeyCode.M))
        {
            keydown = "M";
        }
        // Check for key "Z"
        else if (Input.GetKey(KeyCode.Z))
        {
            keydown = "Z";
        }
        // Check for key "X"
        else if (Input.GetKey(KeyCode.X))
        {
            keydown = "X";
        }
        // Check for key "Q"
        else if (Input.GetKey(KeyCode.Q))
        {
            keydown = "Q";
        }
        // Check for key "W"
        else if (Input.GetKey(KeyCode.W))
        {
            keydown = "W";
        }
        else
        {
            // Return NONE when keys are not pressed
            keydown = "NONE";
        }
        // Debug key to console
        Debug.Log("KEYDOWN DETECTED: " + keydown);
    }
}
