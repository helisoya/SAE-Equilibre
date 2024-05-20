using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class ArduinoInput : MonoBehaviour
{

    private SerialPort port;
    private Coroutine routine;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Animator animator;

    private string[] split;
    private float Accel_X, Accel_Y, Accel_Z, Gyro_x, Gyro_y, Gyro_z;

    private const float COEF_G = 9.80665f; // 1G = COEF_G m/s**2

    void Start()
    {
        port = new SerialPort("COM5", 115200);
        port.ReadTimeout = 5000;
        if (!port.IsOpen)
        {
            try
            {
                port.Open();
            }
            catch (Exception e)
            {
                print(e.Message);
            }

        }
        else
            Debug.LogError("Port already open");

        routine = StartCoroutine(ReadData());
    }



    // Update is called once per frame
    IEnumerator ReadData()
    {
        while (true)
        {
            try
            {
                if (!port.IsOpen)
                    port.Open();

                string ypr = port.ReadLine();
                print(ypr);

                split = ypr.Split(";");

                if (split.Length <= 5)
                {
                    continue;
                }

                Accel_X = float.Parse(split[0], CultureInfo.InvariantCulture);
                Accel_Z = float.Parse(split[1], CultureInfo.InvariantCulture);
                Accel_Y = float.Parse(split[2], CultureInfo.InvariantCulture) - 1f;

                Gyro_x = float.Parse(split[3], CultureInfo.InvariantCulture);
                Gyro_z = float.Parse(split[4], CultureInfo.InvariantCulture);
                Gyro_y = float.Parse(split[5], CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                print(e);
            }


            yield return new WaitForEndOfFrame();
        }
    }

    void Update()
    {
        if (routine != null)
        {
            leftHand.position += leftHand.forward * Accel_X * Time.deltaTime * COEF_G +
            leftHand.up * Accel_Y * Time.deltaTime * COEF_G +
            leftHand.right * Accel_Z * Time.deltaTime * COEF_G;

            leftHand.Rotate(new Vector3(Gyro_x, Gyro_y, Gyro_z) * Time.deltaTime);


            rightHand.position += rightHand.forward * Accel_X * Time.deltaTime * COEF_G +
            rightHand.up * Accel_Y * Time.deltaTime * COEF_G +
            rightHand.right * Accel_Z * Time.deltaTime * COEF_G;

            rightHand.Rotate(new Vector3(Gyro_x, Gyro_y, Gyro_z) * Time.deltaTime);
        }
    }


    void OnApplicationQuit()
    {
        if (port.IsOpen)
        {
            port.Close();
            port.Dispose();
        }

        if (routine != null)
        {
            StopCoroutine(routine);
        }
    }


    void OnAnimatorIK()
    {
        if (animator)
        {
            if (rightHand != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
            }

            if (leftHand != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
            }

        }
    }
}
