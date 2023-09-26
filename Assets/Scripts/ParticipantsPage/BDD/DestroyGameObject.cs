using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{

    public GameObject addedUserInfoTemplate;

    public void Destroy()
    {
        Destroy(addedUserInfoTemplate);
    }

}
