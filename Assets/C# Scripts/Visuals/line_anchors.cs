// Objectives: Anchor a Line Renderer object using for the "Altitude segment" of the Global MR view.
// Dependencies: <> 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line_anchors : MonoBehaviour
{
    // Create a Line Renderer object and set its positions
    [SerializeField] private LineRenderer jointToPlaneLine;
    [SerializeField] private LineRenderer alitudeDisplayLine;

    // Create a Transform object to store the End Effector position
    [SerializeField] private Transform joint6;

    // Create a Transform object to store the Alitude Display Cube of the altitude line
    [SerializeField] private Transform altitudeDisplayCube;

    // Create a Transform object to store the Midpoint Cube of the altitude line
    [SerializeField] private Transform altitudeMidpointCube;

    // Create an empty Vector3 object -> Compute and store altitude plane point
    [SerializeField] private Vector3 planeIntersection = new Vector3(0f, 0f, 0f);

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        // Compute position of the plane intersection point
        planeIntersection = joint6.position - new Vector3(0f, 0.25f, 0f);

        // Update the start and end points of the Line Renderer
        jointToPlaneLine.SetPosition(0, joint6.position);
        jointToPlaneLine.SetPosition(1, planeIntersection);

        // Compute the position of the Midpoint Cube
        //float xPos = (planeIntersection.x - joint6.position.x) / 2;
        //float yPos = (planeIntersection.y - joint6.position.y) / 2;
        //float zPos = (planeIntersection.z - joint6.position.z) / 2;

        float xPos = joint6.position.x;
        float yPos = joint6.position.y - (0.25f/2);
        float zPos = joint6.position.z;

        altitudeMidpointCube.position = new Vector3((xPos), (yPos), (zPos));

        // Update the start and end points of the Line Renderer
        alitudeDisplayLine.SetPosition(0, altitudeDisplayCube.position);
        alitudeDisplayLine.SetPosition(1, altitudeMidpointCube.position);

    }
}
