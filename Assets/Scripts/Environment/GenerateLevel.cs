using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] section;
    public GameObject endSection;
    public int sectionCurrentIndex = 0;
    public int zPos = 40;
    private bool creatingSection = false;
    public int secNum;
    private bool noMoreSectionMakeEnd = false;
    private bool noMoreGenerationToDo = false;
    private bool scriptIsDoneRunning = false;

    private void Start()
    {
        Debug.Log("curDifficulty = " + DifficultyManager.currentDifficulty);
    }

    void Update()
    {
        if (creatingSection == false && !noMoreSectionMakeEnd && !noMoreGenerationToDo)
        {
            creatingSection = true;
            StartCoroutine(GenerateSection());
        }
        
        if (noMoreSectionMakeEnd && !noMoreGenerationToDo)
        {
            StartCoroutine(GenerateEnding());
        }

        if (noMoreGenerationToDo && !scriptIsDoneRunning)
        {
            this.enabled = false;
            this.scriptIsDoneRunning = true;
        }

    }

    IEnumerator GenerateSection()
    {
        secNum = Random.Range(0, 3); // pour les variations non utilisée
        int[] chosenSectionOrder = DifficultyManager.sectionDifficulties[DifficultyManager.currentDifficulty];
        Instantiate(section[chosenSectionOrder[sectionCurrentIndex]], new Vector3(0,0,zPos), Quaternion.identity);
        zPos += 40;
        sectionCurrentIndex += 1;
        noMoreSectionMakeEnd = (sectionCurrentIndex == chosenSectionOrder.Length);
        yield return new WaitForSeconds(1.0f);
        creatingSection = false;
    }

    IEnumerator GenerateEnding()
    {
        Instantiate(endSection, new Vector3(0, 0, zPos), Quaternion.identity);
        noMoreGenerationToDo = true;
        yield return new WaitForSeconds(1.0f);
        
    }

}
