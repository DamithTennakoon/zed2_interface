/* ----------------------------------------------------------
 * Class Name: point_extraction
 * Objective: Access the depth values computed by the ZEDM camera on the CPU and construct a accessible point cloud
 * Written by: Damith Tennakoon
 * Date: 2025-04-29
 * -----------------------------------------------------------
 */

// Default libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class specific libraries
using sl; // StereoLabs

public class point_extraction : MonoBehaviour
{
    // Define details panel objects
    [SerializeField] private ZEDManager ZEDManager;
    private sl.float4 PCValue;
    [SerializeField] private float MaxDistanceThresh = 1.5f; // Units of meters
    [SerializeField] private float MinDistanceThresh = 0.0f; // Units of meters
    public int MaxPoints = 5000; // Maxmimum number of points to be plotted
    public int Stride = 3; // Number of pixels skipped in search alogirithm
    public List<Vector3> PointPositions = new List<Vector3>(); // List to store the positions of the point cloud
    public List<Color> PointColours = new List<Color>(); // List to store the corresponding colour values for each point
    [SerializeField] private bool Logger = false;
    private Dictionary<Vector3Int, Color> VoxelMap = new Dictionary<Vector3Int, Color>(); // Define a map to store the voxel values
    public float VoxelResolution = 0.01f; // Define the size of 1 cube in the voxel map - default 1cm spatial resolution

    // Define ZED objects
    private ZEDCamera ZEDCamera;
    public sl.ZEDMat PointCloud = new sl.ZEDMat();

    void Start()
    {
        // Initialize objects
        ZEDCamera = ZEDManager.zedCamera;
        uint mWidth = (uint)ZEDCamera.ImageWidth;
        uint mHeight = (uint)ZEDCamera.ImageHeight;
        PointCloud.Create(mWidth, mHeight, ZEDMat.MAT_TYPE.MAT_32F_C4, ZEDMat.MEM.MEM_CPU); // Create the point cloud
        Debug.Log("Init res: " + ZEDCamera.ImageWidth + "x" + ZEDCamera.ImageHeight);
    }

    void Update()
    {
        // Retrieve the latest point cloud data and store it to the point cloud materials
        ZEDCamera.RetrieveMeasure(PointCloud, MEASURE.XYZRGBA, ZEDMat.MEM.MEM_CPU, new sl.Resolution((uint)ZEDCamera.ImageWidth, (uint)ZEDCamera.ImageHeight)); // Retrieve the point cloud map for this frame

        // Clear memory of lists to update the point cloud per frame
        PointPositions.Clear();
        PointColours.Clear();

        // Iterate through pcd material and append valid points to list
        for (int y = 0; y < ZEDCamera.ImageHeight; y += Stride)
        {
            for (int x = 0; x < ZEDCamera.ImageWidth; x += Stride)
            {
                if (PointPositions.Count >= MaxPoints) break; // Break out of the inner loop when list is full

                // Retrieve the values from the point cloud material
                PointCloud.GetValue(x, y, out PCValue, ZEDMat.MEM.MEM_CPU); // Extract the XYZRGBA value for this pixel
                Vector3 Position = new Vector3(PCValue.r, PCValue.g, PCValue.b);
                float PackedColour = PCValue.a;
                uint packed = System.BitConverter.ToUInt32(System.BitConverter.GetBytes(PackedColour), 0);

                // Extract individual color bytes using bit masking
                byte r = (byte)((packed >> 0) & 0xFF);  // Red is in the lowest byte
                byte g = (byte)((packed >> 8) & 0xFF);  // Green is next
                byte b = (byte)((packed >> 16) & 0xFF); // Blue is third
                byte a = (byte)((packed >> 24) & 0xFF); // Alpha is highest byte
                Color Colour = new Color(r / 255f, g / 255f, b / 255f, a / 255f); // Convert to Unity Color (0–1 range)

                // Skip to next iteration of the loop the data is not valid
                float Range = Position.magnitude;
                if (!((Range < MaxDistanceThresh) && (Range > MinDistanceThresh)))
                {
                    continue;
                }

                // Append the values to the lists
                PointPositions.Add(Position);
                PointColours.Add(Colour);

                // Convert position vector to a Voxel key in the map
                /*
                Vector3Int VoxelKey = new Vector3Int(
                    Mathf.RoundToInt(Position.x / VoxelResolution),
                    Mathf.RoundToInt(Position.y / VoxelResolution),
                    Mathf.RoundToInt(Position.z / VoxelResolution)
                    );
                // Add the KEY to the map with its correspndonding Colour VALUE pair
                if (!(VoxelMap.ContainsKey(VoxelKey)))
                {
                    VoxelMap[VoxelKey] = Colour;
                }*/
            }
        }   
        
    }

    // FUNCTION: Expose the map to other classes
    public Dictionary<Vector3Int, Color> GetVoxelMap()
    {
        return VoxelMap;
    }
}
