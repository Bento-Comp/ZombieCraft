using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_moneyText = null;


    private void OnEnable()
    {
        MoneyManager.OnMoneyUpdate += OnMoneyUpdate;
    }

    private void OnDisable()
    {
        MoneyManager.OnMoneyUpdate -= OnMoneyUpdate;
    }


    private void OnMoneyUpdate(float amount)
    {
        m_moneyText.text = FormatGameText.FormatValue(amount);
    }
}
