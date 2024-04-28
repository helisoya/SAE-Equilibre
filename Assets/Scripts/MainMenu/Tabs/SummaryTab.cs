using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the summary tab, where the user can see visually all the movements he will have to do in the exercice
/// </summary>
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

        GameManager.instance.PlayVocalAssistantSFX(GameManager.instance.GetVocalAssistantData().summaryClip);

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

    /// <summary>
    /// Deletes the existing icons
    /// </summary>
    void DeleteAllIcons()
    {
        Utils.DestroyChildren(iconsRoot);
    }

    public override void Close()
    {
        base.Close();
        DeleteAllIcons();
    }

    /// <summary>
    /// Changes the current preview
    /// </summary>
    /// <param name="triggerName">The animation's trigger</param>
    /// <param name="animationSpeed">The animation speed</param>
    public void SetPreview(string triggerName, float animationSpeed)
    {
        demoAnimator.speed = animationSpeed;
        demoAnimator.CrossFade(triggerName, 0.1f);
    }

    /// <summary>
    /// Click event for the button that starts the exercice
    /// </summary>
    public void Click_Start()
    {
        MainMenuManager.instance.LoadGameScene(this);
    }

    /// <summary>
    /// Click event for the button that opens the backgrounds tab
    /// </summary>
    public void Click_ToBackgrounds()
    {
        MainMenuManager.instance.StartTransition(this, MainMenuManager.instance.backgroundTab);
    }

}
