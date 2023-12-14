using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconPreview : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    private SummaryTab tab;
    private Sequence sequence;
    private Movement move;

    public void Init(Sequence sequence, SummaryTab tab)
    {
        this.sequence = sequence;
        this.tab = tab;
        move = GameManager.instance.GetMovement(sequence.idMovement);

        iconImage.sprite = move.animationIcon;
    }


    public void OnPointerEnter()
    {
        tab.SetPreview(move.animationTriggerName, move.isContinuous ? 1 : sequence.animationInSeconds);
    }

    public void OnPointerExit()
    {
        tab.SetPreview("Idle", 1);
    }
}
