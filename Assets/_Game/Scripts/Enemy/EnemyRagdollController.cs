using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdollController : MonoBehaviour
{
    public System.Action OnRagdollEnabled;

    [SerializeField]
    private RagdollController m_ragdollController = null;

    [SerializeField]
    private EnemyHealth m_enemyHealth = null;

    [SerializeField]
    private float m_minEjectionStrength = 5f;

    [SerializeField]
    private float m_maxEjectionStrength = 10f;


    private void OnEnable()
    {
        m_enemyHealth.OnEnemyDead += OnEnemyDead;
    }

    private void OnDisable()
    {
        m_enemyHealth.OnEnemyDead -= OnEnemyDead;
    }


    private void OnEnemyDead()
    {
        m_ragdollController.EnableRagdoll();
        OnRagdollEnabled?.Invoke();

        float randomEjectionStrength = Random.Range(m_minEjectionStrength, m_maxEjectionStrength);
        m_ragdollController.ApplyEjectionForceOnRagdoll((Vector3.forward + Vector3.up) * randomEjectionStrength);
    }

}
