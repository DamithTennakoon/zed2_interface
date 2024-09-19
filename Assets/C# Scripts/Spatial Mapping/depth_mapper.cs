// Objective: Utilzize the ZEDManager object to collect depth data for every n-pixels -> LiDAR Map.
// Dependencies:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using sl;
using System;
using System.IO;

public class depth_mapper : MonoBehaviour
{
    // Define ZED objects
    [SerializeField] private ZEDManager zedManager;
    public ZEDCamera zedCamera;

    // Define camera property storage objects
    private int imWidth;
    private int imHeight;
    [SerializeField] private int horizontalStepSize = 8;
    [SerializeField] private int verticalStepSize = 2;

    // Define system objects for CSV files
    private string filePath = "Assets/DepthData.csv";
    private string displacementFilePath = "Assets/displacementData.csv";
    private float elapsedTime = 0f;


    public sl.ZEDMat point_cloud = new sl.ZEDMat();

    public sl.Pose pose = new sl.Pose();
    public sl.SensorsData sensor_data = new sl.SensorsData();

    void Start()
    {
        // Attach the zedCamera parameters to object
        zedCamera = zedManager.zedCamera;

        uint mWidth = (uint)zedCamera.ImageWidth;
        uint mHeight = (uint)zedCamera.ImageHeight;

        point_cloud.Create(mWidth, mHeight, ZEDMat.MAT_TYPE.MAT_32F_C4, ZEDMat.MEM.MEM_CPU);
    }

    // Update is called once per frame
    void Update()
    {
        if (zedCamera.ImageHeight > 500 && Input.GetKey(KeyCode.X))
        {
            // Iteratate through each row and column of the image frame
            for (int y = 0; y < zedCamera.ImageHeight; y += verticalStepSize)
            {
                for (int x = 0; x < zedCamera.ImageWidth; x += horizontalStepSize)
                {
                    // Retrieve depth value of current pixel
                    float depthValue = zedCamera.GetDistanceValue(new Vector3(x, y, 0f)) * 100f; // Convert to cm

                    // Store the point onto CSV file
                    if (depthValue > 10f)
                    {
                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine(x + "," + y + "," + depthValue);
                        }
                    }
                    
                }
            }
        }

        // Attempt to save the point cloud as a PLY object using the ZED function
        if (Input.GetKey(KeyCode.Z))
        {
            // Save file
            zedCamera.SaveCurrentPointCloudInFile(0, "point_cloud_data");
        }

        
        /*
        zedCamera.GetPosition(ref pose, REFERENCE_FRAME.WORLD);
        //Debug.Log("Translation: --> " + "X: " + pose.translation.x* 100f + " Y: " + pose.translation.y * 100f + " Z: " + pose.translation.z * 100f);
        Debug.Log("Verticle Displacement: " + pose.translation.y * 1000f);
        //Debug.Log("Rotation: --> " + pose.rotation);

        // Record the verticle displcements into a .csv file
        if (pose.translation.y * 1000f > 0f)
        {
            using (StreamWriter writer = new StreamWriter(displacementFilePath, true))
            {
                writer.WriteLine(elapsedTime + "," + pose.translation.y);
            }
        } 

        elapsedTime += Time.deltaTime;*/

        /*
        zedCamera.GetInternalSensorsData(ref sensor_data, TIME_REFERENCE.CURRENT);
        Quaternion imu_orientation = sensor_data.imu.fusedOrientation;
        Vector3 acceleration = sensor_data.imu.linearAcceleration;
        Debug.Log("IMU Orientation : " + imu_orientation);
        Debug.Log("Acceleration: " + acceleration);
        Debug.Log("AVAILABILITY: " + sensor_data.imu.available);*/


        /*
        zedCamera.CreateTextureImageType(sl.VIEW.LEFT);
        Texture2D rgbImage = zedCamera.GetTexture(ZEDCamera.TYPE_VIEW.RETRIEVE_IMAGE, 0);
        Color pixelColor = rgbImage.GetPixel(200, 200);
        Debug.Log("Pixel" + pixelColor.r + "," + pixelColor.g + "," + pixelColor.b);
        Texture2D test = zedCamera.CreateTextureImageType(sl.VIEW.LEFT);*/

    }

}
