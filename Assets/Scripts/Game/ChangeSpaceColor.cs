using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeSpaceColor : MonoBehaviour
{
   [SerializeField] private Material Purple;
   [SerializeField] private Material Blue;
   [SerializeField] private Material Red;

   private void Start()
   {

      switch (Random.Range(0, 3))
      {
         case 0:
            RenderSettings.skybox = Purple;
            //Debug.Log("Purple");
            break;
         case 1:
            RenderSettings.skybox = Blue;
            //Debug.Log("Blue");
            break;
         case 2:
            RenderSettings.skybox = Red;
            //Debug.Log("Red");
            break;
      }
   }
}
