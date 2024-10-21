using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyGainedUI : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator = null;

    [SerializeField]
    private TMP_Text m_valueText = null;


    private void OnEnable()
    {
        MoneyManager.OnGainMoney += OnMoneyStackCollected;
    }

    private void OnDisable()
    {
        MoneyManager.OnGainMoney -= OnMoneyStackCollected;
    }


    private void OnMoneyStackCollected(float amount)
    {
        string text = FormatGameText.FormatValue(amount);

        m_valueText.text = "+" + text;

        m_animator.SetTrigger("Appear");
    }

}
