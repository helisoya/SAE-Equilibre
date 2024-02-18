using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents an icon from the summary tab
/// </summary>
public class IconPreview : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    private SummaryTab tab;
    private Sequence sequence;
    private Movement move;

    /// <summary>
    /// Initialize the component
    /// </summary>
    /// <param name="sequence">The linked sequence</param>
    /// <param name="tab">The root tab</param>
    public void Init(Sequence sequence, SummaryTab tab)
    {
        this.sequence = sequence;
        this.tab = tab;
        move = GameManager.instance.GetMovement(sequence.idMovement);

        iconImage.sprite = move.animationIcon;
    }

    /// <summary>
    /// OnPointerEnter event
    /// </summary>
    public void OnPointerEnter()
    {
        tab.SetPreview(move.animationTriggerName, move.isContinuous ? 1 : move.animationLength / sequence.animationInSeconds);
    }

    /// <summary>
    /// OnPointerExit event
    /// </summary>
    public void OnPointerExit()
    {
        tab.SetPreview("Idle", 1);
    }
}
