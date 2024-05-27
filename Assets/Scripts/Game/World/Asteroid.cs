using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    
    public GameObject targetObject;
    
    public float minDistance = 10.0f;
    public float maxDistance = 50.0f;
    
    public float rotationSpeed = 5.0f;
    
    private float randomDistance;
    private Vector3 randomDirection;
    
    void Start()
    {
        // Ensure the targetObject is set
        if (targetObject == null)
        {
            Debug.LogError("Target Object is not set!");
            return;
        }

        // Generate a random distance within the specified range
        randomDistance = Random.Range(minDistance, maxDistance);

        // Generate a random direction (unit vector)
        randomDirection = Random.insideUnitSphere.normalized;
        
        // stet start position
        targetObject.transform.position = new Vector3(0,0,randomDistance)+randomDirection;
    }

    void Update()
    {
        // Rotate the random direction vector over time
        targetObject.transform.RotateAround(this.transform.position, randomDirection, rotationSpeed * Time.deltaTime);
    }
}
