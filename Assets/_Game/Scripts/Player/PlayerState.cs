using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    public static System.Action OnPlayerEnabled;
    public static System.Action OnPlayerDisabled;
    public static System.Action OnPlayerDead;


    private void OnEnable()
    {
        GamePhaseManager.OnTitleScreen += OnTitleScreen;
        GamePhaseManager.OnGameStart += OnGameStart;
        ObstaclePlayerDetector.OnPlayerHitDeadlyObstacle += OnPlayerHitDeadlyObstacle;
    }

    private void OnDisable()
    {
        GamePhaseManager.OnTitleScreen -= OnTitleScreen;
        GamePhaseManager.OnGameStart -= OnGameStart;
        ObstaclePlayerDetector.OnPlayerHitDeadlyObstacle -= OnPlayerHitDeadlyObstacle;
    }


    private void OnTitleScreen()
    {
        OnPlayerDisabled?.Invoke();
    }

    private void OnGameStart()
    {
        OnPlayerEnabled?.Invoke();
    }

    private void OnPlayerHitDeadlyObstacle()
    {
        OnPlayerDisabled?.Invoke();
        OnPlayerDead?.Invoke();
    }
}
