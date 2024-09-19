// Objective: Inherit hand positional data and compute normalize vectors to provide control directions for robot arm
// NOTES: Reference the GitBook documentation on "Communication Structures" for the hand control tranmistted data format.
// Dependencies: <hand_tracking_input.cs>, <gesture_recognition.cs>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compute_hand_control : MonoBehaviour
{
    // Refernce the Hand Tracking Input class
    [SerializeField] private hand_tracking_input handTrackingInput;

    // Reference the Gesture Recognition class -> listen for gesture types and attach to handControlData
    [SerializeField] private gesture_recognition gestureRecognition;

    // Define an empty float array of length 7
    public float[] handControlData = new float[7];

    // Define a GameObject object to store the Control Sphere's positional data
    [SerializeField] private GameObject controlSphere;

    // Define a float object to store the distance betweent the Control Sphere and the right hand
    private float distToRightHand = 0.0f;

    // Define a public boolean variable to store the state of control
    public bool isInsideSphere = true;

    void Start()
    {
        // Initialize values of each index to 0f to ensure robot arm safety
        for (int i = 0; i < handControlData.Length; i++)
        {
            handControlData[i] = 0.0f; 
        }

    }
    void Update()
    {
        // Compute and Store the distance between the Control Sphere and the right hand
        distToRightHand = distanceFromCentre(controlSphere, handTrackingInput.handTrackingData[0], handTrackingInput.handTrackingData[1], handTrackingInput.handTrackingData[2]);

        // TEMP -> Debug the distance of the right hand for resting gesture
        Debug.Log("[INFO] DIST to Centre: " + distToRightHand);

        // Check whether thee right hand model is inside outside the sphere
        if (distToRightHand <= 0.05f)
        {
            // Log to the console STOP mode
            Debug.Log("STOP");

            // Update the global state of the control sphere
            isInsideSphere = true;

            // Set the values of the Hand Control Data to zero - stop robot arm from moving
            for (int i = 0; i < 3; i++)
            {
                handControlData[i] = 0.0f;
            }
        }
        // In the instance the right hand is outside the control sphere
        else if ((distToRightHand > 0.05f) && (distToRightHand <= 0.3f))
        {
            // Update the global state of the control sphere
            isInsideSphere = false;

            // Normalize the vector joining the palm transform and the centre of the control sphere
            Vector3 normalizedRightHand = normalizeVector(controlSphere, handTrackingInput.handTrackingData[0], handTrackingInput.handTrackingData[1], handTrackingInput.handTrackingData[2]);

            for (int i = 0; i < 3; i++)
            {
                // NOTE: Currently, the axis of the normalized values are inverse so a negative should be multiplied - Not sure what is causing the axis inverse -> TO: DO
                handControlData[i] = -1.0f * normalizedRightHand[i];
            }

            // Log thee distance of the right hand to the centre of the sphere
            //Debug.Log("Distance: " + distToRightHand + " Sum: " + (Vector3.Magnitude(normalizedRightHand)));
        }
        // Set all hand control values to zero when the right hand is in the resting position (distance greater than 0.3)
        else
        {
            // Log to the console STOP mode
            Debug.Log("STOP");

            // Set the values of the Hand Control Data to zero - stop robot arm from moving
            for (int i = 0; i < 3; i++)
            {
                handControlData[i] = 0.0f;
            }
        }

        // UPDATE GESTURES
        // Boolean value for LEFT HAND PINCH
        //handControlData[6] = gestureRecognition.leftHandPinchState ? 1.0f : 0.0f; // Ternary operator

        // Boolean value for LEFT HAND GRIP
        //handControlData[6] = gestureRecognition.leftHandGripState ? 1.0f : 0.0f; // Ternary operator

        // Integer value for RIGHT HAND VARIABLE GRIP
        handControlData[6] = gestureRecognition.scaledRightVariablePinch;

    }

    // METHOD: Return the distance between a Transform object and the position of another object
    // IN: (GameObjeect, float x, float y, float z)
    private float distanceFromCentre(GameObject CentreObject, float x, float y, float z)
    {
        // Construct a local Vector3 object from the x, y, z position
        Vector3 targetObject = new Vector3(x, y, z);

        // Construct a local Vector3 object for the Centre Object
        Vector3 centreObject = new Vector3(CentreObject.transform.position.x, CentreObject.transform.position.y, CentreObject.transform.position.z);

        // Compute distanc between both vectors
        float distanceFromCentre = Vector3.Distance(centreObject, targetObject);

        // Return the distance
        return distanceFromCentre;
    }

    // METHOD: Return a normalized Vector3 object for a object's position in 3D space
    // IN: (GameObject, float x, float y, float z)
    private Vector3 normalizeVector(GameObject CentreObject, float x, float y, float z)
    {
        // Construct a Vector3 object from the input positional data
        Vector3 globalPosition = new Vector3(x, y, z);

        // Compute relative position vector to the Control Sphere
        Vector3 relativePosition = CentreObject.transform.position - globalPosition;

        // Compute normalized vector
        Vector3 nomarlizedVector = Vector3.Normalize(relativePosition);

        // Return the normalized vector
        return nomarlizedVector;
    }
}
