using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummaryTab : MainMenuTab
{
    [Header("Summary")]
    [SerializeField] private Transform iconsRoot;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Animator demoAnimator;


    public override void Open()
    {
        base.Open();
        DeleteAllIcons();

        List<Sequence> sequences = GameManager.instance.currentExercice.sequences;
        List<string> done = new List<string>();

        foreach (Sequence sequence in sequences)
        {
            if (done.Contains(sequence.idMovement)) continue;
            done.Add(sequence.idMovement);
            Instantiate(iconPrefab, iconsRoot).GetComponent<IconPreview>().Init(sequence, this);
        }

        iconsRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            (iconPrefab.GetComponent<RectTransform>().sizeDelta.x + 5) * done.Count,
            iconsRoot.GetComponent<RectTransform>().sizeDelta.y
        );
    }

    void DeleteAllIcons()
    {
        foreach (Transform child in iconsRoot)
        {
            Destroy(child.gameObject);
        }
    }

    public override void Close()
    {
        base.Close();
        DeleteAllIcons();
    }

    public void SetPreview(string triggerName, float animationSpeed)
    {
        demoAnimator.speed = animationSpeed;
        demoAnimator.CrossFade(triggerName, 0.1f);
    }


    public void Click_Start()
    {
        SceneManager.LoadScene(MainMenuManager.instance.chosenScene);
    }

    public void Click_ToBackgrounds()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.backgroundTab);
    }

}
