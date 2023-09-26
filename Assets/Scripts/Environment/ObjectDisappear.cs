using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisappear : MonoBehaviour
{
    public GameObject testObject;

    public void OnTriggerEnter(Collider other)
    {
        this.testObject.GetComponent<BoxCollider>().enabled = false;
        testObject.SetActive(false);
    }
}
