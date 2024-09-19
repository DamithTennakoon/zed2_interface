using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform targetObject;  // Specify the target object to revolve around
    public float rotationSpeed = 5f; // Adjust the rotation speed as needed

    private float initialDistance;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target object not assigned. Please assign a target object in the inspector.");
            return;
        }

        // Calculate the initial distance between the objects
        initialDistance = Vector3.Distance(transform.position, targetObject.position);
    }

    void Update()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target object not assigned. Please assign a target object in the inspector.");
            return;
        }

        // Calculate the desired position in a circular orbit
        Vector3 offset = new Vector3(Mathf.Sin(Time.time * rotationSpeed), 0f, Mathf.Cos(Time.time * rotationSpeed)) * initialDistance;
        Vector3 desiredPosition = targetObject.position + offset;

        // Smoothly move the object towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

        // Ensure the object is always looking at the target
        transform.LookAt(targetObject);
    }
}
