// Objective: Enable hand gesture controls in the Global Vision scene
// Dependencies: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global_hand_gestures : MonoBehaviour
{
    // Create a transform object to store the orientation vector of the wrist joint
    [SerializeField] private Transform joint0;

    // Create transform objects to store the orientation vectors of tip finger joints
    [SerializeField] private Transform joint4;
    [SerializeField] private Transform joint8;
    [SerializeField] private Transform joint12;
    [SerializeField] private Transform joint16;
    [SerializeField] private Transform joint20;

    // Create a float variable to store the distance between Joint 16 and Joint 0 -> <0.062 to detect 
    [SerializeField] private float fistDistance = 1.0f;

    // TEMP -> Create a Boolean object to store the state of the Fist Gesture
    public bool fistGestureState = false;

    // Create a Boolean object to store the state of the Pinch Gesture 
    public bool rightPinchState = false;

    // Define a float variable to store the threshold distance to detect a fist gesture
    private float fistGestureThresh = 0.065f;

    // Define a float variable to store the threshold distance to detect a fist gesture
    private float pinchGestureThresh = 0.007f;

    // Define a Corouting object and a callback timer to compute gestures less frequently
    private Coroutine gestureDetectionCoroutine;
    private float gestureDetectionCallbackTimer = 0.3f;
    void Start()
    {
        // Beging the pinch gesture detection coroutine
        gestureDetectionCoroutine = StartCoroutine(GestureDetection());
    }

    // Update is called once per frame
    void Update()
    {
        fistDistance = Vector3.Distance(joint16.position, joint0.position);

        // TEMP -> Store the state of the Fist Gesture
        if (fistDistance <= fistGestureThresh)
        {
            // Set fist gesture detection to true
            fistGestureState = true;
        }
        else
        {
            // Set fist gesture detection to false
            fistGestureState = false;
        }

    }

    IEnumerator GestureDetection()
    {
        while (true)
        {
            // Compute the distance between the thumber and index tip joints
            float thumbIndexDist = Vector3.Distance(joint4.position, joint8.position);

            // Check if the threshold distance is met to detect a left hand pinch
            if (thumbIndexDist < pinchGestureThresh)
            {
                // Flip the sate of the boolean
                rightPinchState = !rightPinchState;
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
