using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a cell of the grid that can be interacted with
/// </summary>
public class FormButtonCase : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite spriteUnchecked;
    [SerializeField] private Sprite spriteChecked;
    private FormRow row;
    private int column;

    /// <summary>
    /// Initialize the cell
    /// </summary>
    /// <param name="row">The linked row</param>
    /// <param name="column">The linked column</param>
    public void Init(FormRow row, int column)
    {
        this.row = row;
        this.column = column;
        RefreshSprite();
    }

    /// <summary>
    /// Refreshs the sprite used in the cell
    /// </summary>
    void RefreshSprite()
    {
        image.sprite = row.columnsSucceded[column] ? spriteChecked : spriteUnchecked;
    }

    /// <summary>
    /// OnClick event for swaping between done/not done
    /// </summary>
    public void Click()
    {
        row.columnsSucceded[column] = !row.columnsSucceded[column];
        RefreshSprite();
    }
}
