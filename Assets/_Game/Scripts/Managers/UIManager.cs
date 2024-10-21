using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private int m_screenIndex = 0;

    [SerializeField]
    private List<GameObject> m_screenList = null;


    private int m_currentIndex;


    private void OnEnable()
    {
        GamePhaseManager.OnTitleScreen += OnTitleScreen;
        GamePhaseManager.OnGameStart+= OnGameStart;
        GamePhaseManager.OnGameOver+= OnGameOver;
    }

    private void OnDisable()
    {
        GamePhaseManager.OnTitleScreen -= OnTitleScreen;
        GamePhaseManager.OnGameStart -= OnGameStart;
        GamePhaseManager.OnGameOver -= OnGameOver;
    }


    private void Update()
    {
        m_screenIndex = Mathf.Clamp(m_screenIndex, 0, m_screenList.Count - 1);

        if (m_screenIndex == m_currentIndex)
            return;

        m_currentIndex = m_screenIndex;

        for (int i = 0; i < m_screenList.Count; i++)
        {
            m_screenList[i].SetActive(i == m_currentIndex);
        }
    }


    private void OnTitleScreen()
    {
        m_screenIndex = 0;
    }

    private void OnGameStart()
    {
        m_screenIndex = 1;
    }

    private void OnGameOver()
    {
        m_screenIndex = 2;
    }

}
