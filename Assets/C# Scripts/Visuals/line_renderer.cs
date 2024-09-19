// Goal: Render a line between two input positions during run-time

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line_renderer : MonoBehaviour
{
    // Define Line Renderer object to store the line
    public LineRenderer lineCentreToHand;

    // Deinfe the Transform objects to store the start and end points of the line
    public Transform position1;
    public Transform position2;

    void Start()
    {
        // Initialize the number of the points the Line Renderer object will take
        lineCentreToHand.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the start and end points as the Transforms move during run-time
        lineCentreToHand.SetPosition(0, position1.position);
        lineCentreToHand.SetPosition(1, position2.position);
    }
}
