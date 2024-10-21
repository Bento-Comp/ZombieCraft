using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PlayerVisualWeaponController : MonoBehaviour
{
    public System.Action OnChangeWeaponModel;
    // float : progression towards next weapon; int : weaponNumber
    public static System.Action<float, int> OnSendYearsProgression;

    [SerializeField]
    private Animator m_handAnimator = null;

    [SerializeField]
    private List<GameObject> m_weaponModelList = null;

    [SerializeField]
    private float m_yearsThreshold = 10f;

    [SerializeField]
    private int m_desiredWeaponIndex = 0;

    private float m_initialYears;
    private int m_currentWeaponIndex;


    private void Awake()
    {
        m_currentWeaponIndex = 0;

        ChangeWeapon(m_currentWeaponIndex);
    }

    private void OnEnable()
    {
        PlayerStat_Years.OnSendInitialYear += OnSendInitialYear;
        PlayerStat_Years.OnUpdateYearsStat += OnUpdateYearsStat;
    }

    private void OnDisable()
    {
        PlayerStat_Years.OnSendInitialYear -= OnSendInitialYear;
        PlayerStat_Years.OnUpdateYearsStat -= OnUpdateYearsStat;
    }


    private void Update()
    {
        m_desiredWeaponIndex = Mathf.Clamp(m_desiredWeaponIndex, 0, m_weaponModelList.Count - 1);

        if (m_handAnimator == null)
            return;

        if (m_weaponModelList.Count == 0 || m_weaponModelList == null)
            return;

        if (m_currentWeaponIndex != m_desiredWeaponIndex)
        {
            m_currentWeaponIndex = m_desiredWeaponIndex;
            ChangeWeapon(m_currentWeaponIndex);
        }
    }


    private void OnSendInitialYear(float initialYears)
    {
        m_initialYears = initialYears;
    }

    private void OnUpdateYearsStat(float currentYears, float yearGains)
    {
        float difference = currentYears - m_initialYears;

        if (difference < 0)
            return;

        int thresholdCrossed = (int)(difference / m_yearsThreshold);

        thresholdCrossed = Mathf.Clamp(thresholdCrossed, 0, m_weaponModelList.Count - 1);

        m_desiredWeaponIndex = thresholdCrossed;

        float yearsProgression = (difference - (m_yearsThreshold * thresholdCrossed)) / m_yearsThreshold;

        OnSendYearsProgression?.Invoke(yearsProgression, thresholdCrossed);
    }


    private void ChangeWeapon(int weaponIndex)
    {
        // + 1 because of how it is managed in the animator
        m_handAnimator.SetInteger("WeaponNumber", weaponIndex + 1);

        for (int i = 0; i < m_weaponModelList.Count; i++)
        {
            m_weaponModelList[i].SetActive(i == weaponIndex);
        }

        OnChangeWeaponModel?.Invoke();
    }



}
