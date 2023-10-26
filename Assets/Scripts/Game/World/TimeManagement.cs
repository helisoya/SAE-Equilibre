using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagement : MonoBehaviour
{
    // Variable deffinition
    [SerializeField] private Transform directionalLight;
    [Range(0, 24)] public int sceneTime = 12;


    // Start is called before the first frame update
    void Start()
    {
        directionalLight.rotation = new Quaternion(0, 0, 0, 0);
        float angle = hourToLightAngle(sceneTime);
        Debug.Log(angle);
        directionalLight.Rotate(angle, 0, 0);
    }

    private float hourToLightAngle(int hour)
    {
        return (float)((hour % 24) * 15.0 / 2);
    }

}