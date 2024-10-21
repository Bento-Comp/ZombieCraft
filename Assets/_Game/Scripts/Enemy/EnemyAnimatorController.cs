using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator = null;

    [SerializeField]
    private EnemyHealth m_enemyHealth = null;


    private void OnEnable()
    {
        m_enemyHealth.OnBulletHit += OnBulletHit;
    }

    private void OnDisable()
    {
        m_enemyHealth.OnBulletHit -= OnBulletHit;
    }

    private void OnBulletHit()
    {
        m_animator.SetTrigger("GetHit");
    }
}
