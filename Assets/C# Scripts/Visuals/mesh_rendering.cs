// Objective: Turn on/off the <MeshRenderer> component of objects in the HRI interface based on states.
// Dependencies: compute_hand_control.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesh_rendering : MonoBehaviour
{
    // Create an instance of the compute_hand_control class
    [SerializeField] private compute_hand_control computeHandControl;

    // Define target GameObjects
    [SerializeField] private GameObject controlSphereBoundary;
    [SerializeField] private GameObject outerSphere;

    // Update is called once per frame
    void Update()
    {
        // Check if the user's hand is inside or outside the sphere
        if (computeHandControl.isInsideSphere == true)
        {
            // If isnide -> Turn on the Outer Sphere object + turn off the Control Sphere Boundary object
            outerSphere.SetActive(true);
            controlSphereBoundary.SetActive(false);
        }
        else
        {
            // If outside -> Turn off the Outer Sphere object + turn on the Control Sphere Boundary object
            outerSphere.SetActive(false);
            controlSphereBoundary.SetActive(true);
        }
    }
}
