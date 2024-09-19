// Goal: Genreate hand skeleton visualize + toggle for switching off joint and skeleton visualizers

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand_joint_renderer : MonoBehaviour
{
    // Define Line Renderer objects to store each set of skeleton visuals
    [SerializeField] private LineRenderer rightThumbLineRenderer;
    [SerializeField] private LineRenderer rightIndexLineRenderer;
    [SerializeField] private LineRenderer rightMiddleLineRenderer;
    [SerializeField] private LineRenderer rightRingLineRenderer;
    [SerializeField] private LineRenderer rightPinkyLineRenderer;

    // Define Transform arrays to store joints of each finger (relative to wrist joint)
    [SerializeField] private Transform[] rightThumbJointTransforms = new Transform[5];
    [SerializeField] private Transform[] rightIndexJointTransforms = new Transform[5];
    [SerializeField] private Transform[] rightMiddleJointTransforms = new Transform[5];
    [SerializeField] private Transform[] rightRingJointTransforms = new Transform[5];
    [SerializeField] private Transform[] rightPinkyJointTransforms = new Transform[5];

    void Start()
    {
        // Initialize the number of counts of each Line Renderer
        rightThumbLineRenderer.positionCount = rightThumbJointTransforms.Length;
        rightIndexLineRenderer.positionCount = rightIndexJointTransforms.Length;
        rightMiddleLineRenderer.positionCount = rightMiddleJointTransforms.Length;
        rightRingLineRenderer.positionCount = rightRingJointTransforms.Length;
        rightPinkyLineRenderer.positionCount = rightPinkyJointTransforms.Length;

    }

    // Update is called once per frame
    void Update()
    {
        // Update the positions of the joints connecting the line for the thumb finger
        for (int i = 0; i < rightThumbJointTransforms.Length; i++)
        {
            rightThumbLineRenderer.SetPosition(i, rightThumbJointTransforms[i].position);
            rightIndexLineRenderer.SetPosition(i, rightIndexJointTransforms[i].position);
            rightMiddleLineRenderer.SetPosition(i, rightMiddleJointTransforms[i].position);
            rightRingLineRenderer.SetPosition(i, rightRingJointTransforms[i].position);
            rightPinkyLineRenderer.SetPosition(i, rightPinkyJointTransforms[i].position);
        }
    }
}
