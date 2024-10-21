using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator = null;

    [SerializeField]
    private PlayerShoot m_playerShoot = null;


    private void OnEnable()
    {
        m_playerShoot.OnPlayerShoot += OnPlayerShoot;

        ObstaclePlayerDetector.OnPlayerHitDeadlyObstacle += OnPlayerHitDeadlyObstacle;
    }

    private void OnDisable()
    {
        m_playerShoot.OnPlayerShoot -= OnPlayerShoot;

        ObstaclePlayerDetector.OnPlayerHitDeadlyObstacle -= OnPlayerHitDeadlyObstacle;
    }

    private void OnPlayerHitDeadlyObstacle()
    {
        m_animator.SetTrigger("Death");
    }

    private void OnPlayerShoot()
    {
        m_animator.SetTrigger("Shoot");
    }

}
