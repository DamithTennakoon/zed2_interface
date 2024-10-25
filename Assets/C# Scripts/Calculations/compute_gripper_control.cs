// Objective: Compute the instances of gripper open/close gesture.
// Dependencies: <joint_storage.cs>,
// Author: Damith Tennakoon
// NOTE: AS A PART OF HRI VERSION II (SECOND YEAR OF MASTER'S)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compute_gripper_control : MonoBehaviour
{
    // Create an instance of joint_storage class
    [SerializeField] private joint_storage jointStorage;

    // Store joint-joint distance values
    [SerializeField] private float dist8_0_rh = 0.0f;
    [SerializeField] private float dist12_0_rh = 0.0f;
    [SerializeField] private float dist16_0_rh = 0.0f;
    [SerializeField] private float dist20_0_rh = 0.0f;

    // Define indication objects
    [SerializeField] private GameObject flagSphere;

    // Define material objects
    [SerializeField] private Material purpleMat;

    // Define gripper gesture thresholds
    private float dist8_0_rh_thresh = 0.09f;
    private float dist12_0_rh_thresh = 0.09f;
    private float dist16_0_rh_thresh = 0.09f;
    private float dist20_0_rh_thresh = 0.09f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
