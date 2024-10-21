using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUIController : MonoBehaviour
{

    [SerializeField]
    private GameObject m_healthUI = null;

    [SerializeField]
    private TMP_Text m_healthText = null;

    [SerializeField]
    private EnemyHealth m_enemyHealth = null;


    private void OnEnable()
    {
        m_enemyHealth.OnUpdateHealth += OnUpdateHealth;
        m_enemyHealth.OnEnemyDead += OnEnemyDead;
    }

    private void OnDisable()
    {
        m_enemyHealth.OnUpdateHealth -= OnUpdateHealth;
        m_enemyHealth.OnEnemyDead -= OnEnemyDead;
    }


    private void Start()
    {
        m_healthUI.SetActive(true);
    }


    private void OnUpdateHealth(float healthValue)
    {
        m_healthText.text = healthValue.ToString("F0");
    }

    private void OnEnemyDead()
    {
        m_healthUI.SetActive(false);
    }

}
