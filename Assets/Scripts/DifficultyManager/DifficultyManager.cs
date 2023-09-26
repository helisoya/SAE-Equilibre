using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance;
    public static int currentDifficulty = 0;

    public static int[] testSectionOrder = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }; 
    // Si aucune difficulté est choisi, lancer un scénario de test

    public static int[] hardSectionOrder = new int[] { 0, 2, 3, 3, 5, 0, 6, 6, 7, 7, 
                                                       0, 0, 0, 4, 4, 1, 1, 3, 1, 1, 
                                                       3, 9, 8, 0, 0, 
                                                       0, 5, 5, 6, 7, 6, 7, 2, 2, 1, 
                                                       0, 1, 1, 1, 1 }; // Missing Section10? 0,0,0,0,0,0,9,0,0,0

    public static int[] easySectionOrder = new int[] { 0, 1, 1, 1, 0, 0, 7, 0, 
                                                       0, 7, 7, 0, 1, 1, 1, 1, 
                                                       0, 1, 5, 1, 0, 5, 5, 5, 
                                                       1, 1, 0, 0, 5, 1, 5, 1, 
                                                       0, 1, 1, 1, 1, 1, 1, 0, 
                                                       0, 0, 5, 5, 5, 5, 5, 0, 
                                                       1, 5, 0 }; 

    public static int[] normalSectionOrder = new int[] { 0, 6, 6, 0, 5, 5, 0, 
                                                         1, 1, 0, 1, 1, 4, 0, 4, 
                                                         0, 1, 1, 9, 
                                                         0, 8, 8, 8, 8, 0, 1, 1, 0, 
                                                         5, 5 };


    public static int[][] sectionDifficulties = new int[][] { testSectionOrder, easySectionOrder, normalSectionOrder, hardSectionOrder };

    private void Awake()
    {
        // S'il y a déjà une instance du difficulty manager, qui n'est pas celle-ci, on la détruit

        if (instance != null && instance != this)
        {
            Debug.LogError("L'instance du DifficultyManager a été détruite!");
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}


