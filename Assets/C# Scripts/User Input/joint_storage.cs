// Objective: Attach the key joints, for left and right hand, and make transform data globall accessible.
// Dependencies: <NONE>
// Witten by: Damith Tennakoon

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joint_storage : MonoBehaviour
{
    // Create a list of Transforms to store each joint
    // List stores joints in order of joint number - aligns with the Joints Documentation Diagram
    public Transform[] rightHandJoints = new Transform[21];

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
