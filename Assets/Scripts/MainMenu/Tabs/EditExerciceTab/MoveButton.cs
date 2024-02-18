using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Represents a button in the EditExercice tab, that is used to add new movements to the  edited exercice
/// </summary>
public class MoveButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveNameText;
    private EditExerciceTab tab;
    private Movement movement;

    /// <summary>
    /// Initialize the component
    /// </summary>
    /// <param name="movement">The linked movement</param>
    /// <param name="tab">The root tab</param>
    public void Init(Movement movement, EditExerciceTab tab)
    {
        this.movement = movement;
        this.tab = tab;

        moveNameText.text = movement.movementName;
    }

    /// <summary>
    /// OnClick event
    /// </summary>
    public void Click()
    {
        tab.Click_AddMovement(movement);
    }

    /// <summary>
    /// OnPointerEnter event
    /// </summary>
    public void OnPointerEnter()
    {
        tab.SetPreview(movement.animationTriggerName, 1);
    }

    /// <summary>
    /// OnPointerExit event
    /// </summary>
    public void OnPointerExit()
    {
        tab.SetPreview("Idle", 1);
    }
}
