using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private Gradient grad;

    public void UpdateGraphics(int currentHp, int maxHp)
    {
        lifeText.text = $"HP: {currentHp}/{maxHp}";
        fill.fillAmount = (float)currentHp / maxHp;
        fill.color = grad.Evaluate((float)currentHp / maxHp);
    }
}
