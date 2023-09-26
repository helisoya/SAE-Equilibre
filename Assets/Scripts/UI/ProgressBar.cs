using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class ProgressBar : MonoBehaviour
{

    [SerializeField]
    private Image ProgressImage;
    [SerializeField]
    private float DefaultSpeed = 1f;
    [SerializeField]
    private UnityEvent<float> OnProgress;
    [SerializeField]
    private UnityEvent OnCompleted;

    // private Coroutine AnimationCoroutine;

    private void Start()
    {
        if (ProgressImage.type != Image.Type.Filled)
        {
            Debug.LogError($"{name}'s ProgressImage n'est pas du type \"Filled\" et ne peut pas être utilisée" + 
                           $" comme barre de progression. Désactivation de cette barre de progression");
            this.enabled = false;

            // Pour montrer des objets dans l'éditeur on peut faire de type if et utiliser PingObject.
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(this.gameObject);
#endif
        }
    }

    public void SetProgress(float Progress)
    {
        SetProgress(Progress, DefaultSpeed);
    }

    public void SetProgress(float Progress, float Speed)
    {
        if (Progress < 0 || Progress > 1)
        {
            Debug.LogWarning($"Valeur invalide donée, il faut une valeur entre 0 et 1. Pas -> {Progress}.");
        }

        ProgressImage.fillAmount = Progress;
        OnProgress?.Invoke(Progress);
        OnCompleted?.Invoke();
    }


    // Code trouvée dans un guide, notre barre de progression n'est pas aussi compliqué

    /*
    public void SetProgress(float Progress, float Speed)
    {
        if (Progress < 0 || Progress > 1)
        {
            Debug.LogWarning($"Invalid progress passed, excepted value is between 0 and 1. Got {Progress}. Clamping");
            Progress = Mathf.Clamp01(Progress);
        }
        if (Progress != ProgressImage.fillAmount)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(AnimateProgress(Progress, Speed));
        }
    }

    private IEnumerator AnimateProgress(float Progress, float Speed)
    {
        float initialProgress = ProgressImage.fillAmount;

        while (time < 1 )
        {
            ProgressImage.fillAmount = Mathf.Lerp(initialProgress, Progress, time);
            time += Time.deltaTime * Speed;

            OnProgress?.Invoke(ProgressImage.fillAmount);
            yield return null;
        }

        Debug.Log("Yes I'm changing the fill amount to : " + Progress);
        ProgressImage.fillAmount = Progress;
        OnProgress?.Invoke(Progress);
        OnCompleted?.Invoke();
    }
    */
}
