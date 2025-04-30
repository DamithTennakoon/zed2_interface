/* ----------------------------------------------------------
 * Class Name: voxel_renderer
 * Objective: Populate the corresponding particle system by accessing the lists created in the point_exraction class.
 * Written by: Damith Tennakoon
 * Date: 2025-04-30
 * -----------------------------------------------------------
 */

// Default libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensure particle system is attached to the same Game Object as this script 
[RequireComponent(typeof(ParticleSystem))]

public class voxel_renderer : MonoBehaviour
{
    // Define user parameters
    ParticleSystem System;
    ParticleSystem.Particle[] Voxels;
    bool VoxelsUpdated = false;
    public float VoxelScale = 0.001f;
    public float Scale = 1f;
    public point_extraction PointExtraction;

    
    private void Start()
    {
        // Initialize components
        System = GetComponent<ParticleSystem>();
    }

    
    private void Update()
    {
        // Update the particle system
        if (VoxelsUpdated)
        {
            System.SetParticles(Voxels, Voxels.Length);
            VoxelsUpdated = false;
        }

        SetVoxels();
    }

    // FUNCTION: Update the values we want to set for the particle system
    public void SetVoxels()
    {
        if (PointExtraction)
        {
            
            if ((PointExtraction.PointPositions.Count == PointExtraction.PointColours.Count))
            {
                // Set the Voxel particle array to the same length as the Positions list
                Voxels = new ParticleSystem.Particle[PointExtraction.PointPositions.Count];
                for (int i = 0; i < PointExtraction.PointPositions.Count; i++)
                {
                    // Set each of the values of the Voxel array to the point and colours
                    Voxels[i].position = PointExtraction.PointPositions[i] * Scale;
                    Voxels[i].startColor = PointExtraction.PointColours[i];
                    Voxels[i].startSize = VoxelScale;
                }
                VoxelsUpdated = true;
            }

            /*
            Dictionary<Vector3Int, Color> VoxelMap = PointExtraction.GetVoxelMap();
            if (Voxels == null || Voxels.Length != VoxelMap.Count)
                Voxels = new ParticleSystem.Particle[VoxelMap.Count];

            int j = 0;

            foreach (var kvp in VoxelMap)
            {
                Vector3 Pos = new Vector3(kvp.Key.x, kvp.Key.y, kvp.Key.z) * PointExtraction.VoxelResolution;
                Color Colour = kvp.Value;

                Voxels[j].position = Pos * Scale;
                Voxels[j].startColor = Colour;
                Voxels[j].startSize = VoxelScale;
                j++;
            }

            VoxelsUpdated = true;*/
        }

        
    }
}
