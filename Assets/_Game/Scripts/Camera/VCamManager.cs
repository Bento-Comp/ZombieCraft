using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_menuCamera = null;

    [SerializeField]
    private GameObject m_inGameCamera = null;



    private void OnEnable()
    {
        GamePhaseManager.OnTitleScreen += OnTitleScreen;
        GamePhaseManager.OnGameStart += OnGameStart;
    }

    private void OnDisable()
    {
        GamePhaseManager.OnTitleScreen -= OnTitleScreen;
        GamePhaseManager.OnGameStart -= OnGameStart;
    }


    private void Start()
    {
        m_menuCamera.SetActive(true);
        m_inGameCamera.SetActive(false);
    }

    private void OnTitleScreen()
    {
        m_menuCamera.SetActive(true);
        m_inGameCamera.SetActive(false);
    }


    private void OnGameStart()
    {
        m_menuCamera.SetActive(false);
        m_inGameCamera.SetActive(true);
    }

}
