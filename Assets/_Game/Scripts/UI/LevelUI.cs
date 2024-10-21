using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_levelText = null;


    private void OnEnable()
    {
        LevelManager.OnSendCurrentLevel += OnSendCurrentLevel;
    }

    private void OnDisable()
    {
        LevelManager.OnSendCurrentLevel -= OnSendCurrentLevel;
    }


    private void OnSendCurrentLevel(int level)
    {
        m_levelText.text = "Level " + level.ToString("F0");
    }
}
