using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public System.Action OnPlayerShoot;
    public static System.Action OnPlayerShoot_Global;
    public static System.Action<GameObject, float> OnSendBulletInfos;

    [SerializeField]
    private GameObject m_bulletPrefab = null;

    [SerializeField]
    private Transform m_bulletSpawnPosition = null;

    [SerializeField]
    private PlayerStat_FireRate m_playerStateFireRate = null;

    [SerializeField]
    private PlayerStat_FireRange m_playerStatFireRange = null;


    private GameObject m_instantiatedBullet;
    private float m_shootRate = 1f;
    private float m_cooldownDuration { get => 1f / m_shootRate; }
    private float m_cooldownTimer;
    private bool m_isCoolingDown;
    private bool m_canShoot;


    private void OnEnable()
    {
        PlayerState.OnPlayerEnabled += OnPlayerEnabled;
        PlayerState.OnPlayerDisabled += OnPlayerDisabled;
    }

    private void OnDisable()
    {
        PlayerState.OnPlayerEnabled -= OnPlayerEnabled;
        PlayerState.OnPlayerDisabled -= OnPlayerDisabled;
    }

    private void Update()
    {
        TryShoot();
        ManageShootCooldown();
    }


    private void OnPlayerEnabled()
    {
        Initialize();
    }

    private void OnPlayerDisabled()
    {
        m_canShoot = false;
    }

    private void Initialize()
    {
        m_isCoolingDown = true;
        m_canShoot = true;
    }


    private void TryShoot()
    {
        if (m_isCoolingDown || m_canShoot == false)
            return;

        Shoot();
    }


    private void Shoot()
    {
        m_shootRate = m_playerStateFireRate.StatValue;

        m_instantiatedBullet = Instantiate(m_bulletPrefab, m_bulletSpawnPosition.position, Quaternion.identity);

        float shootRange = m_playerStatFireRange.StatValue;

        OnSendBulletInfos?.Invoke(m_instantiatedBullet, shootRange);

        OnPlayerShoot?.Invoke();

        m_isCoolingDown = true;
    }


    private void ManageShootCooldown()
    {
        if (m_isCoolingDown == false)
            return;

        m_cooldownTimer += Time.deltaTime;

        if(m_cooldownTimer > m_cooldownDuration)
        {
            m_isCoolingDown = false;
            m_cooldownTimer = 0;
        }
    }
}
