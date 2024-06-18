using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using TMPro;
using UnityEngine;

public class ArduinoControllerPunch : MonoBehaviour
{
    private SerialPort port;
    private Coroutine routine;
    [SerializeField] private Transform rotateCube;
    [SerializeField] private LineRenderer lineRendererArduino;
    [SerializeField] private LineRenderer lineRendererTarget;
    [SerializeField] private TextMeshProUGUI textMax;
    [SerializeField] private float gravityValue = 0;
    [SerializeField] private float maxAngle;


    [SerializeField] private bool changeTarget = true;
    [SerializeField]
    private Vector3[] targets = {
        Vector3.forward,
        Vector3.up,
        Vector3.right,
        -Vector3.right,
        -Vector3.up
    };

    private Vector3 target = Vector3.forward;
    private float lastGood = 0;

    private string[] split;
    private float Accel_X, Accel_Y, Accel_Z, Gyro_x, Gyro_y, Gyro_z;

    void Start()
    {
        target = Vector3.forward;

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
                print("Data : " + ypr);

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
        rotateCube.rotation = Quaternion.identity;
        gravityValue = Accel_Y;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Init();
        }

        lineRendererTarget.SetPosition(1, target * 3);

        rotateCube.Rotate(new Vector3(Gyro_x, Gyro_y, Gyro_z) * Time.deltaTime);

        Vector3 vectorArduino = new Vector3(Accel_X, Accel_Y, Accel_Z) - Vector3.up * gravityValue;

        lineRendererArduino.SetPosition(1, vectorArduino);

        float angle = Vector3.Angle(target, vectorArduino);
        print("Angle : " + angle);

        bool didAPunch = angle <= maxAngle && Time.time - lastGood >= 1; // Angle OK + Au moins 1 seconde après le dernier punch

        textMax.text = didAPunch ? "Punch !" : "";
        if (didAPunch) lastGood = Time.time;

        if (didAPunch && changeTarget)
        {
            target = targets[UnityEngine.Random.Range(0, targets.Length)];
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
}
