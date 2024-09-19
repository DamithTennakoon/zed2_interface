// Objective: Compute the position of the Control Sphere Boundary object.
// Dependencies: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_boundary : MonoBehaviour
{
    // Define Tranform objects to store the start and end vectors
    [SerializeField] private Transform endTransform;
    [SerializeField] private Transform startTransform;

    // Define GameObject to store the Control Sphere Boundary object
    [SerializeField] private GameObject controlSphereBoundary;

    // Define float object to store the radius of the Control Sphere boundary
    private float controlSphereRadius;

    void Start()
    {
        // Initialize the Control Sphere radius
        controlSphereRadius = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the direction of the control sphere boundary object
        controlSphereBoundary.transform.position = startTransform.position + controlSphereRadius * normalizeVector(startTransform, endTransform);
        controlSphereBoundary.transform.LookAt(endTransform);

        //Debug.Log("DISTANCR: " + Vector3.Distance(endTransform.position, startTransform.position));
    }

    // Define method to compute the normalized unit vector - global coordaintes
    Vector3 normalizeVector(Transform startPose, Transform endPose)
    {
        // Compute relative vector
        Vector3 relativeVector = endPose.position - startPose.position;

        // Compute manginute of the relative vector
        Vector3 normVector = Vector3.Normalize(relativeVector);
        //Vector3 normVector = relativeVector / Vector3.Magnitude(relativeVector);

        // Return the unit vector - global coordinates
        return normVector;
    }
}
