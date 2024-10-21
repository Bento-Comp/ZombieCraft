using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public System.Action<float> OnUpdateHealth;
    public System.Action OnBulletHit;
    public System.Action OnEnemyDead;

    [SerializeField]
    private GameObject m_rootObject = null;

    [SerializeField]
    private Collider m_bulletDetectorCollider = null;

    // param
    [SerializeField]
    private float m_maxHealth = 5;

    [SerializeField]
    private float m_minRandomHealth = 3;

    [SerializeField]
    private float m_maxRandomHealth = 7;

    private float m_currentHealth;



    private void OnEnable()
    {
        BulletController.OnHitEnemy += OnBulletDetected;
        SegmentProceduralLevelElements.OnProceduralLevelElementGenerated += OnProceduralLevelElementGenerated;
    }

    private void OnDisable()
    {
        BulletController.OnHitEnemy -= OnBulletDetected;
        SegmentProceduralLevelElements.OnProceduralLevelElementGenerated -= OnProceduralLevelElementGenerated;
    }


    private void Start()
    {
        Initialize();
    }


    private void Initialize()
    {
        m_currentHealth = m_maxHealth;
        OnUpdateHealth?.Invoke(m_currentHealth);
        m_bulletDetectorCollider.enabled = true;
    }

    private void OnProceduralLevelElementGenerated(GameObject levelElement)
    {
        if (m_rootObject == levelElement)
        {
            DetermineRandomHealth();
        }
    }

    private void DetermineRandomHealth()
    {
        m_currentHealth = Random.Range(m_minRandomHealth, m_maxRandomHealth);
        OnUpdateHealth?.Invoke(m_currentHealth);
    }

    private void OnBulletDetected(GameObject bulletDetectorCollider)
    {
        if (m_bulletDetectorCollider.gameObject != bulletDetectorCollider)
            return;

        OnBulletHit?.Invoke();

        m_currentHealth--;
        OnUpdateHealth?.Invoke(m_currentHealth);

        if (m_currentHealth <= 0)
        {
            BulletController.OnHitEnemy -= OnBulletDetected;
            m_bulletDetectorCollider.enabled = false;
            OnEnemyDead?.Invoke();
        }
    }

}
