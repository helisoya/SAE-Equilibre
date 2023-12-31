using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Tabs")]
    public ParticipantsTab participantsTab;
    public TitleTab titleTab;
    public OptionsTab optionsTab;
    public MusicsTab musicsTab;
    public ExercicesTab exercicesTab;
    public BackgroundTab backgroundTab;
    public UsersTab usersTab;
    public EditExercicesListTab editExercicesListTab;
    public EditExerciceTab editExerciceTab;
    public CreditsTab creditsTab;
    public SummaryTab summaryTab;


    [HideInInspector] public Exercice editedExercice;
    [HideInInspector] public string chosenScene;


    [Header("Transition")]
    [SerializeField] private RawImage transitionImg;
    [SerializeField] private Animator transitionAnimator;
    public static MainMenuManager instance;
    private Texture2D renderedTexture;
    private bool startTransition = false;
    private MainMenuTab fromTab;
    private MainMenuTab toTab;

    void Awake()
    {
        instance = this;
    }

    public void StartTransition(MainMenuTab from, MainMenuTab to)
    {
        fromTab = from;
        toTab = to;
        startTransition = true;
    }

    IEnumerator RecordFrame()
    {
        yield return new WaitForEndOfFrame();
        transitionImg.gameObject.SetActive(false);

        int width = Screen.width;
        int height = Screen.height;

        renderedTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        renderedTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        renderedTexture.Apply();

        transitionImg.gameObject.SetActive(true);

        transitionImg.texture = renderedTexture;
        transitionAnimator.SetTrigger("Transition");

        fromTab.Close();
        toTab.Open();
    }

    public void LateUpdate()
    {
        if (startTransition)
        {
            startTransition = false;
            StartCoroutine(RecordFrame());
        }
    }
}
