using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentMoveComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveNameText;
    [SerializeField] private TMP_InputField inputFieldNumberTimes;
    [SerializeField] private TMP_InputField inputFieldAnimationLength;
    [SerializeField] private TextMeshProUGUI numberTimesLabel;

    private EditExerciceTab tab;
    private Sequence sequence;
    private Movement movement;

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

    public void Click_Delete()
    {
        tab.DeleteSequence(sequence, gameObject);
    }

    public void OnPointerEnter()
    {
        tab.SetPreview(movement.animationTriggerName,
         movement.isContinuous ? 1 : movement.animationLength / sequence.animationInSeconds);
    }

    public void OnPointerExit()
    {
        tab.SetPreview("Idle", 1);
    }
}
