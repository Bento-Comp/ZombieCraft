using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFxController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_hitFx = null;

    [SerializeField]
    private ParticleSystem m_deathFx = null;

    [SerializeField]
    private EnemyHealth m_enemyHealth = null;




    private void OnEnable()
    {
        m_enemyHealth.OnBulletHit += OnBulletHit;
        m_enemyHealth.OnEnemyDead += OnEnemyDead;
    }

    private void OnDisable()
    {
        m_enemyHealth.OnBulletHit -= OnBulletHit;
        m_enemyHealth.OnEnemyDead -= OnEnemyDead;
    }

    private void Start()
    {
        m_hitFx.Stop();
        m_deathFx.Stop();
    }


    private void OnBulletHit()
    {
        m_hitFx.Play();
    }

    private void OnEnemyDead()
    {
        m_deathFx.Play();
    }

}
