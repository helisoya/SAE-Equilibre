using System.Collections;
using TMPro;
using UnityEngine;

public class ProgressBarHandler : MonoBehaviour
{
    [SerializeField]
    private ProgressBar ProgressBar;
    public GameObject player;
    private float playerDistance;
    private float oldPlayerDistance;
    private int curDif;
    private float totalDistance;

    private void Start()
    {

        curDif = DifficultyManager.currentDifficulty;
        totalDistance = (float)(((DifficultyManager.sectionDifficulties[curDif].Length+1.2)*40)/1.0);
        // Debug.Log("Total is : " + totalDistance);

        StartCoroutine(SetInitialProgress(ProgressBar));
            
    }

    void Update()
    {
        playerDistance = player.transform.position.z;
        if (playerDistance > 0.0 && !(playerDistance == oldPlayerDistance))
        {
            ProgressBar.SetProgress(playerDistance / totalDistance);
            // Debug.Log("curProgressValue = " + (playerDistance/totalDistance));
        }
        oldPlayerDistance = playerDistance;
        if (playerDistance >= totalDistance)
        {
            this.enabled = false;
        }
    }

    private IEnumerator SetInitialProgress(ProgressBar ProgressBar)
    {
        ProgressBar.SetProgress(0, float.MaxValue);
        yield return null;
    }

}