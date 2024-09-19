// Objective: Recognize simple hand gestures, using deterministic algorithms, and publish state paramters.
// Dependencies: <hand_tracking_input.cs>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gesture_recognition : MonoBehaviour
{
    // Construct an instance of the hand_tracking_input class
    [SerializeField] private hand_tracking_input handTrackingInput;

    // Define thresholds for gestures
    private float leftHandPinchThresh = 0.007f; // Unit: MM
    private float leftHandGripThresh = 0.062f;

    // Define Boolean variable to store the state of the left hand pinch gesture (flippable)
    public bool leftHandPinchState = false;
    public bool leftHandGripState = false;

    // TEMP -> Define global float object to store the distance between the Left Hand Joint 8 and Joint 0
    public float leftRingPalmDist = 0f;

    public float rightVariablePinch = 0f;
    public float scaledRightVariablePinch = 0f;
    private float minVariablePinchVal = 0.008f;
    private float maxVariablePinchVal = 0.1f;

    // Define a Corouting object and a callback timer to compute gestures less frequently
    private Coroutine gestureDetectionCoroutine;
    private float gestureDetectionCallbackTimer = 0.6f; // CHANGE TO 0.6f after variable gripper controller


    void Start()
    {
        // Beging the gesture detection coroutine
        gestureDetectionCoroutine = StartCoroutine(GestureDetection());
    }

    // Update is called once per frame
    void Update()
    {
        // Debug the LEFT HAND PINCH GESTURE state
        Debug.Log("LEFT HAND PINCH STATE: " + leftHandPinchState);
    }

    // Perform gesture recognition algorithms
    IEnumerator GestureDetection()
    {
        while (true)
        {
            // LEFT HAND PINCH GESTURE
            // Compute the distance between the thumber and index tip joints
            float leftThumbIndexDist = Vector3.Distance(handTrackingInput.leftThumbJoint4.position, handTrackingInput.leftIndexJoint8.position);

            // Check if the threshold distance is met to detect a left hand pinch
            if (leftThumbIndexDist < leftHandPinchThresh)
            {
                // Flip the sate of the boolean
                leftHandPinchState = !leftHandPinchState;
            }

            // LEFT HAND GRIP GESTURE
            // Compute the distance between the index and palm joints
            leftRingPalmDist = Vector3.Distance(handTrackingInput.leftRingJoint16.position, handTrackingInput.leftPalmJoint0.position);

            // Check if the threshold sitance is met to detect a left hand grip
            if (leftRingPalmDist < leftHandGripThresh)
            {
                // TEMP: Debug grip
                leftHandGripState = !leftHandGripState;
            }

            // VARIABLE RIGHT HAND PINCH GESTURE (used for variable gripper control)
            rightVariablePinch = Vector3.Distance(handTrackingInput.rightThumbJoint4.position, handTrackingInput.rightIndexJoint8.position); // MAX =  0.1 (255) , MIN = 0.009 (0)
            float remappedPinchVal = 255f * (rightVariablePinch - minVariablePinchVal) / (maxVariablePinchVal - minVariablePinchVal);

            if (remappedPinchVal >= 255f)
            {
                scaledRightVariablePinch = 255f;
            }
            else if (remappedPinchVal <= 0f)
            {
                scaledRightVariablePinch = 0f;
            }
            else
            {
                scaledRightVariablePinch = remappedPinchVal;
            }

            // Callback timer
            yield return new WaitForSeconds(gestureDetectionCallbackTimer);
        }
    }

    // Destroy the coroutine object when the application is killed
    void OnDestroy()
    {
        // Stop the coroutine when the GameObject is destroyed
        if (gestureDetectionCoroutine != null)
            StopCoroutine(gestureDetectionCoroutine);
    }
}
