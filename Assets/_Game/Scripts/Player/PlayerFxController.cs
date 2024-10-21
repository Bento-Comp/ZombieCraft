using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFxController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_shootFx = null;

    [SerializeField]
    private ParticleSystem m_levelUpFx = null;

    [SerializeField]
    private PlayerShoot m_playerShoot = null;

    [SerializeField]
    private PlayerVisualWeaponController m_playerVisualWeaponController = null;


    private bool m_canShowLevelUpFx;


    private void OnEnable()
    {
        m_playerShoot.OnPlayerShoot += OnPlayerShoot;
        m_playerVisualWeaponController.OnChangeWeaponModel += OnChangeWeaponModel;
    }

    private void OnDisable()
    {
        m_playerShoot.OnPlayerShoot -= OnPlayerShoot;
        m_playerVisualWeaponController.OnChangeWeaponModel -= OnChangeWeaponModel;
    }


    private void Start()
    {
        m_shootFx.Stop();
        m_levelUpFx.Stop();

        StartCoroutine(DelayLevelUpFxUsageCoroutine());
    }


    private void OnChangeWeaponModel()
    {
        if(m_canShowLevelUpFx)
        m_levelUpFx.Play();
    }

    private void OnPlayerShoot()
    {
        m_shootFx.Play();
    }


    // This coroutine prevents level up fx to play at the launch of the game when data is loaded
    // When the year stat is loaded, it sets the correct weapon to display and to prevent the level up fx to play, here is the coroutine
    private IEnumerator DelayLevelUpFxUsageCoroutine()
    {
        m_canShowLevelUpFx = false;
        yield return new WaitForSeconds(2f);
        m_canShowLevelUpFx = true;
    }

}
