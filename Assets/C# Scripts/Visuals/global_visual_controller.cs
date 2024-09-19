// Objective: Control the on/off states of the visuals in the Global MR view
// Dependencies: <global_hand_gestures.cs>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global_visual_controller : MonoBehaviour
{
    // Construct an instance of the global_hand_gestures class -> Turn on/off visuals
    [SerializeField] private global_hand_gestures globalHandGestures;

    // Create GameObject object to store canvas
    [SerializeField] private GameObject robotInfoCanvas;

    // Create Material Objects to store solid and transparent materials
    [SerializeField] private Material solidBlackMat;
    [SerializeField] private Material transparentBlackMat;

    // Create GameObjects to store the various 3D mesh models of the arm
    [SerializeField] private GameObject[] robotArmModels = new GameObject[6];

    // Create GameObjects to store the the Orientation Gizmos
    [SerializeField] private GameObject[] orientationGizmos = new GameObject[6];

    // Create GameObjects to store the To Surface Cube and Altitude Midpoint Cube
    [SerializeField] private GameObject toSurfaceCube;
    [SerializeField] private GameObject altitudeMidpointCube;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Change state of robot arm visuals
        if (globalHandGestures.rightPinchState == true)
        {
            // Turn on Robot Info Canvas
            robotInfoCanvas.SetActive(true);

            // Turn on Transparent Materials for each Robot Arm part
            for (int i = 0; i <= robotArmModels.Length; i++)
            {
                robotArmModels[i].GetComponent<MeshRenderer>().material = transparentBlackMat;

                orientationGizmos[i].SetActive(true);
            }

            // Turn on Altitude visuals
            toSurfaceCube.SetActive(true);
            altitudeMidpointCube.SetActive(true);
        }
        else
        {
            // Turn off visuals
            robotInfoCanvas.SetActive(false);

            // Turn on Solid Materials for each Robot Arm part
            for (int i = 0; i <= robotArmModels.Length; i++)
            {
                robotArmModels[i].GetComponent<MeshRenderer>().material = solidBlackMat;

                orientationGizmos[i].SetActive(false);
            }

            // Turn off Altitude visuals 
            toSurfaceCube.SetActive(false);
            altitudeMidpointCube.SetActive(false); 
        }
    }
}
