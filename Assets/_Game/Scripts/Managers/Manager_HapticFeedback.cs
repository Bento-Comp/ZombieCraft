using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_HapticFeedback : MonoBehaviour
{



    private void OnEnable()
    {
        Taptic.tapticOn = true;

        PlayerShoot.OnPlayerShoot_Global += OnPlayerShoot_Global;
        Portal.OnBulletHitPortal += OnBulletHitPortal;
        ObstaclePlayerDetector.OnPlayerHitObstacle += OnPlayerHitObstacle;
        ObstaclePlayerDetector.OnPlayerHitDeadlyObstacle += OnPlayerHitDeadlyObstacle;
        Gate.OnPlayerEnterGate_Global += OnPlayerEnterGate;
    }

    private void OnDisable()
    {
        Taptic.tapticOn = false;

        PlayerShoot.OnPlayerShoot_Global -= OnPlayerShoot_Global;
        Portal.OnBulletHitPortal -= OnBulletHitPortal;
        ObstaclePlayerDetector.OnPlayerHitObstacle -= OnPlayerHitObstacle;
        ObstaclePlayerDetector.OnPlayerHitDeadlyObstacle -= OnPlayerHitDeadlyObstacle;
        Gate.OnPlayerEnterGate_Global -= OnPlayerEnterGate;
    }


    private void OnPlayerShoot_Global()
    {
        Taptic.Light();
    }

    private void OnPlayerEnterGate(float bonusAmount, int gateNumber)
    {
        Taptic.Light();
    }

    private void OnBulletHitPortal()
    {
        Taptic.Light();
    }

    private void OnPlayerHitDeadlyObstacle()
    {
        Taptic.Heavy();
    }

    private void OnPlayerHitObstacle()
    {
        Taptic.Heavy();
    }

}
