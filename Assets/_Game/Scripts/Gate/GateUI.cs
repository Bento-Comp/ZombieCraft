using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateUI : MonoBehaviour
{
    [SerializeField]
    private Gate m_gate = null;

    [SerializeField]
    private List<GameObject> m_gate1UI = null;

    [SerializeField]
    private List<GameObject> m_gate1EnabledImageList = null;

    [SerializeField]
    private List<GameObject> m_gate2UI = null;

    [SerializeField]
    private List<GameObject> m_gate2EnabledImageList = null;

    [SerializeField]
    private List<GameObject> m_gate3UI = null;

    [SerializeField]
    private List<GameObject> m_gate3EnabledImageList = null;


    [SerializeField]
    private TMP_Text m_gate1BonusText = null;

    [SerializeField]
    private TMP_Text m_gate2BonusText = null;

    [SerializeField]
    private TMP_Text m_gate3BonusText = null;


    private void OnEnable()
    {
        m_gate.OnSendGatesBonus += OnSendGatesBonus;

        m_gate.OnEnableGate += OnEnableGate;

        m_gate.OnPlayerEnterGate += OnPlayerEnterGate;
    }

    private void OnDisable()
    {
        m_gate.OnSendGatesBonus -= OnSendGatesBonus;

        m_gate.OnEnableGate -= OnEnableGate;

        m_gate.OnPlayerEnterGate -= OnPlayerEnterGate;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        ToggleObjectInList(m_gate1EnabledImageList, false);
        ToggleObjectInList(m_gate2EnabledImageList, false);
        ToggleObjectInList(m_gate3EnabledImageList, false);
    }

    private void OnSendGatesBonus(float bonus1, float bonus2, float bonus3)
    {
        m_gate1BonusText.text = "+" + bonus1.ToString("F0") + " Years";
        m_gate2BonusText.text = "+" + bonus2.ToString("F0") + " Years";
        m_gate3BonusText.text = "+" + bonus3.ToString("F0") + " Years";
    }

    private void OnEnableGate(int gateNumber)
    {
        if (gateNumber == 1)
            ToggleObjectInList(m_gate1EnabledImageList, true);
        else if (gateNumber == 2)
            ToggleObjectInList(m_gate2EnabledImageList, true);
        else if (gateNumber == 3)
            ToggleObjectInList(m_gate3EnabledImageList, true);
    }


    private void ToggleObjectInList(List<GameObject> list, bool state)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(state);
        }
    }


    private void OnPlayerEnterGate(float bonus, int gateNumber)
    {
        if (gateNumber == 1)
            ToggleObjectInList(m_gate1UI, false);
        else if (gateNumber == 2)
            ToggleObjectInList(m_gate2UI, false);
        else if (gateNumber == 3)
            ToggleObjectInList(m_gate3UI, false);
    }

}
