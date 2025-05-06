using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class vfx_voxel_mapper : MonoBehaviour
{
    public point_extraction extractor; // Assign in Inspector
    private VisualEffect vfx;
    private GraphicsBuffer positionBuffer;
    private int currentBufferSize = 0;

    void Start()
    {
        vfx = GetComponent<VisualEffect>();
    }

    void Update()
    {
        List<Vector3> positions = extractor.PointPositions;

        if (positions == null || positions.Count == 0)
        {
            vfx.SetInt("pointCount", 0);
            return;
        }

        // Resize buffer only if needed
        if (positionBuffer == null || currentBufferSize != positions.Count)
        {
            if (positionBuffer != null)
                positionBuffer.Release();

            // Allocate the buffer with Vector3 size (3 floats)
            positionBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, positions.Count, sizeof(float) * 3);
            currentBufferSize = positions.Count;

            Debug.Log("Created buffer for " + currentBufferSize + " points.");
        }

        // Push data to GPU
        positionBuffer.SetData(positions);

        // Send buffer and metadata to the VFX graph
        vfx.SetGraphicsBuffer("positionBuffer", positionBuffer);
        vfx.SetInt("pointCount", currentBufferSize);
    }

    void OnDestroy()
    {
        if (positionBuffer != null)
            positionBuffer.Release();
    }
}
