using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    TitleScreen,
    InGame,
    Gameover
}

public class GamePhaseManager : MonoBehaviour
{
    public static System.Action OnTitleScreen;
    public static System.Action OnGameStart;
    public static System.Action OnGameOver;
    public static System.Action OnReturnToTitleScreen;



    private GamePhase m_currentGamePhase;


    private void OnEnable()
    {
        PlayerState.OnPlayerDead += OnPlayerDead;
    }

    private void OnDisable()
    {
        PlayerState.OnPlayerDead -= OnPlayerDead;
    }


    private void Start()
    {
        Initialize();
    }


    private void Initialize()
    {
        m_currentGamePhase = GamePhase.TitleScreen;
        OnTitleScreen?.Invoke();
    }


    private void OnPlayerDead()
    {
        m_currentGamePhase = GamePhase.Gameover;
        OnGameOver?.Invoke();
    }


    public void StartGame()
    {
        OnGameStart?.Invoke();
        m_currentGamePhase = GamePhase.InGame;
    }

    public void ReturnToTitleScreen()
    {
        OnReturnToTitleScreen?.Invoke();
        m_currentGamePhase = GamePhase.TitleScreen;
    }



}
