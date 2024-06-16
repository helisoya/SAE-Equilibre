using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class ArduinoControllerTracker : MonoBehaviour
{
    private SerialPort port;
    private Coroutine routine;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Animator animator;
    private string[] split;
    private float Accel_X, Accel_Y, Accel_Z, Gyro_x, Gyro_y, Gyro_z;
    private float Speed_X, Speed_Y, Speed_Z;
    private Vector3 leftStartPos, rightStartPos;
    [SerializeField] private float gravityValue = 0;

    private const float COEF_G = 1f; // 1G = 9.80665f m/s**2
    private const float ACCEL_DAMP = 0.6f;



    void Start()
    {
        leftStartPos = leftHand.localPosition;
        rightStartPos = rightHand.localPosition;

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
                Accel_Y = float.Parse(split[2], CultureInfo.InvariantCulture) /* - 1f*/;

                Gyro_x = float.Parse(split[3], CultureInfo.InvariantCulture);
                Gyro_z = float.Parse(split[4], CultureInfo.InvariantCulture);
                Gyro_y = float.Parse(split[5], CultureInfo.InvariantCulture);

                Speed_X += Accel_X;
                Speed_Y += Accel_Y;
                Speed_Z += Accel_Z;
            }
            catch (Exception e)
            {
                print(e);
            }


            yield return new WaitForEndOfFrame();
        }
    }

    void Init()
    {
        leftHand.rotation = Quaternion.identity;
        rightHand.rotation = Quaternion.identity;
        leftHand.localPosition = leftStartPos;
        rightHand.localPosition = rightStartPos;
        gravityValue = Accel_Y;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Init();
        }

        if (routine != null)
        {
            leftHand.Rotate(new Vector3(Gyro_x, Gyro_y, Gyro_z) * Time.deltaTime);
            rightHand.Rotate(new Vector3(Gyro_x, Gyro_y, Gyro_z) * Time.deltaTime);

            leftHand.position += leftHand.forward * Speed_X * Time.deltaTime * COEF_G +
            leftHand.up * Speed_Y * Time.deltaTime * COEF_G +
            leftHand.right * Speed_Z * Time.deltaTime * COEF_G
            - Vector3.up * Time.deltaTime * gravityValue * COEF_G;

            rightHand.position += rightHand.forward * Speed_X * Time.deltaTime * COEF_G +
            rightHand.up * Speed_Y * Time.deltaTime * COEF_G +
            rightHand.right * Speed_Z * Time.deltaTime * COEF_G
            - Vector3.up * Time.deltaTime * gravityValue * COEF_G;


            Speed_X *= ACCEL_DAMP * Time.deltaTime;
            Speed_Y *= ACCEL_DAMP * Time.deltaTime;
            Speed_Z *= ACCEL_DAMP * Time.deltaTime;

            print("Speed : " + Speed_X + " " + Speed_Y + " " + Speed_Z);
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
