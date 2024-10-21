using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortalVisual : MonoBehaviour
{
    [SerializeField]
    private Portal m_gate = null;

    [SerializeField]
    private TMP_Text m_titleText = null;

    [SerializeField]
    private TMP_Text m_valueText = null;

    [SerializeField]
    private TMP_Text m_impactValueText = null;

    [SerializeField]
    private MeshRenderer m_gateModel = null;

    [SerializeField]
    private Material[] m_positiveBorderMaterial = null;

    [SerializeField]
    private Material[] m_negativeBorderMaterial = null;


    private void OnEnable()
    {
        m_gate.OnSendCurrentValueForVisual += UpdateGateVisual;
        m_gate.OnSendImpactValueForVisual += OnSendCurrentValueForVisual;
        m_gate.OnSendTitleForVisual += OnSendTitleForVisual;
    }

    private void OnDisable()
    {
        m_gate.OnSendCurrentValueForVisual -= UpdateGateVisual;
        m_gate.OnSendImpactValueForVisual -= OnSendCurrentValueForVisual;
        m_gate.OnSendTitleForVisual -= OnSendTitleForVisual;
    }

    private void OnSendTitleForVisual(PlayerStatType playerStatType)
    {
        switch (playerStatType)
        {
            case PlayerStatType.Years:
                m_titleText.text = "Years";
                break;
            case PlayerStatType.FireRange:
                m_titleText.text = "Fire Range";
                break;
            case PlayerStatType.FireRate:
                m_titleText.text = "Fire Rate";
                break;
            default:
                break;
        }
    }

    private void OnSendCurrentValueForVisual(float impactOnValue)
    {
        m_impactValueText.text = impactOnValue.ToString("F0");
    }

    private void UpdateGateVisual(float value)
    {
        if (value < 0)
        {
            m_gateModel.materials = m_negativeBorderMaterial;
        }
        else
        {
            m_gateModel.materials = m_positiveBorderMaterial;
        }

        m_valueText.text = value.ToString("F0");

    }
}
