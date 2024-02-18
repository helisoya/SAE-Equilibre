using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Represents a sequence from an edited exercice. 
/// Used in the EditExercice tab to modify it's linked sequence
/// </summary>
public class CurrentMoveComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveNameText;
    [SerializeField] private TMP_InputField inputFieldNumberTimes;
    [SerializeField] private TMP_InputField inputFieldAnimationLength;
    [SerializeField] private TextMeshProUGUI numberTimesLabel;

    private EditExerciceTab tab;
    private Sequence sequence;
    private Movement movement;

    /// <summary>
    /// Initialize the component
    /// </summary>
    /// <param name="sequence">The linked sequence</param>
    /// <param name="movement">The linked sequence's movement</param>
    /// <param name="tab">The root tab</param>
    public void Init(Sequence sequence, Movement movement, EditExerciceTab tab)
    {
        this.sequence = sequence;
        this.tab = tab;
        this.movement = movement;

        numberTimesLabel.text = movement.isContinuous ? "Temps : " : "Nb répétition :";


        moveNameText.text = movement.movementName;

        inputFieldAnimationLength.SetTextWithoutNotify(sequence.animationInSeconds.ToString());
        inputFieldNumberTimes.SetTextWithoutNotify(sequence.movementTime.ToString());

        inputFieldAnimationLength.gameObject.SetActive(!movement.isContinuous);

    }

    /// <summary>
    /// Event for changing the sequence's time (Number of repeats / Total time, depending on the type of movement)
    /// </summary>
    public void Event_TypeNumberTimes()
    {
        int parsed;
        if (!int.TryParse(inputFieldNumberTimes.text, out parsed) || parsed <= 0 || parsed >= 50)
        {
            inputFieldNumberTimes.SetTextWithoutNotify("1");
            sequence.movementTime = 1;
        }
        else
        {
            sequence.movementTime = parsed;
        }
    }

    /// <summary>
    /// Event for changing the animation's length (Repeated movements only)
    /// </summary>
    public void Event_TypeAnimationLength()
    {
        int parsed;
        if (!int.TryParse(inputFieldAnimationLength.text, out parsed) || parsed <= 0 || parsed >= 15)
        {
            inputFieldAnimationLength.SetTextWithoutNotify("1");
            sequence.animationInSeconds = 1;
        }
        else
        {
            sequence.animationInSeconds = parsed;
        }
        OnPointerEnter();
    }

    /// <summary>
    /// Click event for deleting the linked sequence
    /// </summary>
    public void Click_Delete()
    {
        tab.DeleteSequence(sequence, gameObject);
    }

    /// <summary>
    /// OnPointerEnter event
    /// </summary>
    public void OnPointerEnter()
    {
        tab.SetPreview(movement.animationTriggerName,
         movement.isContinuous ? 1 : movement.animationLength / sequence.animationInSeconds);
    }

    /// <summary>
    /// OnPointerExit event
    /// </summary>
    public void OnPointerExit()
    {
        tab.SetPreview("Idle", 1);
    }
}
