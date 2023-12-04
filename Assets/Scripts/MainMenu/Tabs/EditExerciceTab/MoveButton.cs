using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveNameText;
    private EditExerciceTab tab;
    private Movement movement;

    public void Init(Movement movement, EditExerciceTab tab)
    {
        this.movement = movement;
        this.tab = tab;

        moveNameText.text = movement.movementName;
    }

    public void Click()
    {
        tab.Click_AddMovement(movement);
    }

    public void OnPointerEnter()
    {
        tab.SetPreview(movement.animationTriggerName, 1);
    }

    public void OnPointerExit()
    {
        tab.SetPreview("Idle", 1);
    }
}
