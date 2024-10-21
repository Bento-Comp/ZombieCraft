using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public System.Action OnBlockDestroyed;

    [SerializeField]
    private GameObject m_controlledObject = null;

    [SerializeField]
    private Collider m_bulletDetector = null;

    [SerializeField]
    private TMP_Text m_blockHealthText = null;

    [SerializeField]
    private float m_initialHealth = 10f;

    private float m_currentHealth;


    private void OnEnable()
    {
        BulletController.OnHitBlock += OnHitBlock;
    }

    private void OnDisable()
    {
        BulletController.OnHitBlock -= OnHitBlock;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_currentHealth = m_initialHealth;
        UpdateUI();
    }


    private void OnHitBlock(GameObject blockColliderObject)
    {
        if (m_bulletDetector.gameObject != blockColliderObject)
            return;

        m_currentHealth--;

        if (m_currentHealth <= 0)
        {
            OnBlockDestroyed?.Invoke();
            Destroy(m_controlledObject);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        m_blockHealthText.text = m_currentHealth.ToString("F0");
    }
}
