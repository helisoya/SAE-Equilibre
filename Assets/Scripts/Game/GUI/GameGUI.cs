using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    [SerializeField] private ExerciceGUIIcon exerciceImg_current;
    [SerializeField] private ExerciceGUIIcon exerciceImg_next1;
    [SerializeField] private ExerciceGUIIcon exerciceImg_next2;

    public static GameGUI instance;

    void Awake()
    {
        instance = this;
    }

    public void SetCurrentExericeImg(string time, Sprite sprite)
    {
        exerciceImg_current.Refresh(sprite, time);
    }

    public void SetNext1ExericeImg(string time, Sprite sprite)
    {
        exerciceImg_next1.Refresh(sprite, time);
    }

    public void SetNext2ExericeImg(string time, Sprite sprite)
    {
        exerciceImg_next2.Refresh(sprite, time);
    }
}
