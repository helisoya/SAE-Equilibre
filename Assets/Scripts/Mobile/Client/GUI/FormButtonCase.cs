using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormButtonCase : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite spriteUnchecked;
    [SerializeField] private Sprite spriteChecked;
    private FormRow row;
    private int column;

    public void Init(FormRow row, int column)
    {
        this.row = row;
        this.column = column;
        RefreshSprite();
    }

    void RefreshSprite()
    {
        image.sprite = row.columnsSucceded[column] ? spriteChecked : spriteUnchecked;
    }

    public void Click()
    {
        row.columnsSucceded[column] = !row.columnsSucceded[column];
        RefreshSprite();
    }
}
