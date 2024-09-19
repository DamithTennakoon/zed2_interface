// Objective: Reduce the positional tracking error of detected aruco markers by localuzing multiple tracked markers.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aruco_detection : MonoBehaviour
{
    // Define Transform objects to store the Aruco markers targets
    public Transform arucoMarker1;
    public Transform arucoMarker2;

    // Define a object to store the target object
    public GameObject arucoMarker1Object;
    public GameObject arucoMarker2Object;
    public Transform targetObject;
    
    // Define boolean objects to store the detection states of the tracked markers
    public bool arucoMarker1State;
    public bool arucoMarker2State;

    // Define Vector3 objects to store the positions of the initialize objects
    private Vector3 arucoMarker1InitPos;
    private Vector3 arucoMarker2InitPos;

    // Define a global object that stores the computed average position
    public Vector3 averagePosition = new Vector3(0f, 0f, 0f);
    // Define a Quaternion object to store the computed average rotation
    public Quaternion averageRotation = new Quaternion(0, 0, 0, 1); // Identity Quaternion

    // TEMP: Vector3 to adjust the ideal position of the localized object
    public Vector3 targetOffset = new Vector3(0f, 0f, 0f);

    void Start()
    {
        // Initiailize boolean states
        arucoMarker1State = false;
        arucoMarker2State = false;

        // Store the initialized global positions of the two target objects
        arucoMarker1InitPos = arucoMarker1Object.transform.position;
        arucoMarker2InitPos = arucoMarker2Object.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // Update the current state parameters of the markers
        arucoMarker1State = arucoMarker1.gameObject.activeSelf;
        arucoMarker2State = arucoMarker2.gameObject.activeSelf;

        // Compute the average positiona and rotation of tracked Aruco markers
        if ((arucoMarker1State) == true && (arucoMarker2State == true))
        {
            // Average position
            averagePosition = (arucoMarker1.position + arucoMarker2.position) / 2; // utilizes the global position

            // Average rotation
            averageRotation = AverageQuaternion(arucoMarker1.rotation, arucoMarker2.rotation); // utilizes the global rotation

            // Update the global position and rotation of the target object
            targetObject.position = averagePosition + targetOffset;
            targetObject.rotation = averageRotation;
        }
        else
        {
            // Zero position
            targetObject.position = new Vector3(0f, 0f, 0f);
        }

        // Manually handle target objects (prioritize Marker 0 as it has a better experimental track)
        if ((arucoMarker1State) && (arucoMarker2State))
        {
            // Zero the position of the Marker 1 object
            //arucoMarker2Object.transform.position = new Vector3(0f, 0f, 0f);
            //arucoMarker2Object.GetComponent<MeshRenderer>().enabled = false;
            arucoMarker2Object.SetActive(false);
        }
        /*
        else if (!arucoMarker1State && arucoMarker2State)
        {
            // Zero the postion of the Marker 0 object
            arucoMarker1Object.transform.position = new Vector3(0f, 0f, 0f);
        }*/
        else
        {
            // Set their default positions
            //arucoMarker1Object.transform.position = arucoMarker1InitPos;
            //arucoMarker2Object.transform.position = arucoMarker2InitPos;
            //arucoMarker2Object.GetComponent<MeshRenderer>().enabled = true;
            arucoMarker2Object.SetActive(true);
        }

    }

    // Method: Returns the average Quaternion of two quaternions
    private Quaternion AverageQuaternion(Quaternion q1, Quaternion q2)
    {
        // Compute the average Quaternion
        Quaternion averageQuaternion = Quaternion.Slerp(q1, q2, 0.5f);

        // Return Quaternion object
        return averageQuaternion;
    }
}
