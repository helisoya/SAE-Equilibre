using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserSessionStat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveNameText;
    [SerializeField] private Image imageFill;

    public void Init(UserMoveStats stats)
    {
        moveNameText.text = stats.moveName;
        imageFill.fillAmount = (float)(stats.success) / (float)(stats.total);
    }
}
