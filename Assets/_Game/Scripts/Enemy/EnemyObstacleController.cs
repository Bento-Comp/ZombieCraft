using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacleController : MonoBehaviour
{
    [SerializeField]
    private ObstaclePlayerDetector m_obstaclePlayerDetector = null;

    [SerializeField]
    private EnemyHealth m_enemyHealth = null;


    private void OnEnable()
    {
        m_enemyHealth.OnEnemyDead += OnEnemyDead;
    }

    private void OnDisable()
    {
        m_enemyHealth.OnEnemyDead -= OnEnemyDead;
    }


    private void Start()
    {
        Initializee();
    }

    private void Initializee()
    {
        m_obstaclePlayerDetector.enabled = true;
    }

    private void OnEnemyDead()
    {
        m_obstaclePlayerDetector.enabled = false;
    }

}
