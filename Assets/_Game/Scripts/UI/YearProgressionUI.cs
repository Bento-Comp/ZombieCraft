using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YearProgressionUI : MonoBehaviour
{
    [SerializeField]
    private Image m_progressionBar = null;

    [SerializeField]
    private Image m_currentWeaponImage = null;

    [SerializeField]
    private Image m_nextWeaponImage = null;

    [SerializeField]
    private List<Sprite> m_weaponSpriteList = null;

    [SerializeField]
    private Sprite m_defaultSprite = null;

    [SerializeField]
    private Color m_defaultColor = Color.white;


    private void OnEnable()
    {
        PlayerVisualWeaponController.OnSendYearsProgression += OnSendYearsProgression;
    }

    private void OnDisable()
    {
        PlayerVisualWeaponController.OnSendYearsProgression -= OnSendYearsProgression;
    }


    private void OnSendYearsProgression(float progression, int weaponNumber)
    {
        m_progressionBar.fillAmount = progression;

        if (weaponNumber < m_weaponSpriteList.Count)
            m_currentWeaponImage.sprite = m_weaponSpriteList[weaponNumber];

        if (weaponNumber + 1 < m_weaponSpriteList.Count)
        {
            m_nextWeaponImage.color = m_defaultColor;
            m_nextWeaponImage.sprite = m_weaponSpriteList[weaponNumber + 1];
        }
        else
        {
            m_nextWeaponImage.color = Color.black;
            m_nextWeaponImage.sprite = m_defaultSprite;
        }
    }


}
