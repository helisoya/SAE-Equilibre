using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInput : MonoBehaviour
{
    private string[] split;
    private float Accel_X, Accel_Y, Accel_Z, Gyro_x, Gyro_y, Gyro_z;
    void OnMessageArrived(string msg)
    {
        print("Message arrived: " + msg);

        split = msg.Split("|");
    }


    void OnConnectionEvent(bool success)
    {
        print(success ? "Device connected" : "Device disconnected");
    }
}
