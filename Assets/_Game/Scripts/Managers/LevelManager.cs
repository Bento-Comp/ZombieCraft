using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static System.Action<int> OnSendCurrentLevel;

    [SerializeField]
    private List<GameObject> m_levelsPrefabList = null;

    [SerializeField]
    private Transform m_levelsParent = null;

    private GameObject m_currentLevelReference;
    private int m_currentLevel;


    private void Start()
    {
        LoadCurrentLevel();
        InstantiateLevel();
    }

    private void OnEnable()
    {
        GamePhaseManager.OnReturnToTitleScreen += OnReturnToTitleScreen;
    }

    private void OnDisable()
    {
        GamePhaseManager.OnReturnToTitleScreen -= OnReturnToTitleScreen;
    }


    private void SaveCurrentLevel()
    {
        PlayerPrefs.SetInt("Level", m_currentLevel);
        OnSendCurrentLevel?.Invoke(m_currentLevel);
    }

    private void LoadCurrentLevel()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            m_currentLevel = PlayerPrefs.GetInt("Level");
        }
        else
        {
            m_currentLevel = 0;
            SaveCurrentLevel();
        }

        OnSendCurrentLevel?.Invoke(m_currentLevel);
    } 

    private void InstantiateLevel()
    {
        int index = m_currentLevel % m_levelsPrefabList.Count;

        m_currentLevelReference = Instantiate(m_levelsPrefabList[index], m_levelsParent);
    }

    private void OnReturnToTitleScreen()
    {
        m_currentLevel++;
        SaveCurrentLevel();
        SceneManager.LoadScene(0);
    }

}
