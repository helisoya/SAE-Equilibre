using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a GUI icon that shows what to do on the screen
/// </summary>
public class GameExerciceIcon : MonoBehaviour
{
    [Header("Normal")]
    [SerializeField] private Image moveIcon;
    [SerializeField] private RectTransform rectTransform;
    private float speed;
    private float stopAt;

    [Header("Trail")]
    [SerializeField] private Image trailImg;
    private float trailSpeed;
    private bool useTrail = false;
    private bool updateTrail = false;


    [Header("Fading")]

    private bool ignoreUpdate = false;

    /// <summary>
    /// Updates the position of the icon
    /// </summary>
    void Update()
    {
        if (!ignoreUpdate && GameGUI.instance.startedExercice)
        {
            if (updateTrail)
            {
                trailImg.fillAmount = Mathf.Clamp(trailImg.fillAmount - Time.deltaTime / trailSpeed, 0f, 1f);

                if (trailImg.fillAmount == 0)
                {
                    FinishMove();
                }
            }
            else
            {
                rectTransform.anchoredPosition -= new Vector2(speed, 0) * Time.deltaTime;
                if (rectTransform.anchoredPosition.x < stopAt)
                {
                    if (useTrail)
                    {
                        updateTrail = true;
                    }
                    else
                    {
                        FinishMove();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Starts fading the icon (deletes it too, when finished)
    /// </summary>
    void FinishMove()
    {
        ignoreUpdate = true;
        StartCoroutine(Routine_Fading());
    }

    /// <summary>
    /// Routine for the fading of the icon
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator Routine_Fading()
    {
        Color col = moveIcon.color;

        while (col.a > 0)
        {
            col.a = Mathf.Clamp(col.a - Time.deltaTime * 5, 0f, 1f);
            moveIcon.color = col;
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Initialize the icon
    /// </summary>
    /// <param name="speed">Icon's speed</param>
    /// <param name="stopAt">Where the icon should stop at</param>
    /// <param name="sprite">Icon's sprite</param>
    /// <param name="position">Icon's initial position</param>
    public void Init(float speed, float stopAt, Sprite sprite, float position)
    {
        ignoreUpdate = false;
        trailImg.gameObject.SetActive(false);
        rectTransform.anchoredPosition = new Vector3(position, 0);
        this.speed = speed;
        this.stopAt = stopAt;
        moveIcon.sprite = sprite;
    }

    /// <summary>
    /// Initialize the trail of the icon
    /// </summary>
    /// <param name="trailSize">The trail's size</param>
    /// <param name="trailSpeed">The trail's speed</param>
    public void InitTrail(float trailSize, float trailSpeed)
    {
        this.trailSpeed = trailSpeed;
        trailImg.gameObject.SetActive(true);
        useTrail = true;
        trailImg.GetComponent<RectTransform>().sizeDelta = new Vector2(trailSize, 20);
    }
}
