using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_yearsText = null;

    [SerializeField]
    private Animator m_animator = null;

    [SerializeField]
    private TMP_Text m_yearsGainLoseText = null;

    [SerializeField]
    private Color m_gainYearsColor = Color.green;

    [SerializeField]
    private Color m_loseYearsColor = Color.red;


    private void OnEnable()
    {
        PlayerStat_Years.OnUpdateYearsStat += OnUpdateYearsStat;

    }

    private void OnDisable()
    {
        PlayerStat_Years.OnUpdateYearsStat -= OnUpdateYearsStat;
    }



    private void OnUpdateYearsStat(float yearsValue, float yearsGain)
    {
        if(yearsGain == 0)
            m_yearsText.text = yearsValue.ToString("F0");

        StartCoroutine(DelayYearsTextUpdateCoroutine(yearsValue, yearsGain));
    }


    private IEnumerator DelayYearsTextUpdateCoroutine(float targetYearsValue, float yearsGain)
    {
        if (yearsGain == 0)
            yield break;

        m_animator.SetTrigger("GainLoseYears");

        string prefix = yearsGain > 0 ? "+" : "";
        Color color = yearsGain > 0 ? m_gainYearsColor : m_loseYearsColor;
        m_yearsGainLoseText.color = color;
        m_yearsGainLoseText.text = prefix + yearsGain.ToString("F0");

        yield return new WaitForSeconds(0.5f);

        m_yearsText.text = targetYearsValue.ToString("F0");
    }

}
