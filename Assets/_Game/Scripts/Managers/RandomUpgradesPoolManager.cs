using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUpgradesPoolManager : MonoBehaviour
{

    public static RandomUpgradesPoolManager Instance;

    private List<PlayerStatType> m_playerStatTypeList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }


    private void OnEnable()
    {
        ResetPool();
    }


    private void ResetPool()
    {
        m_playerStatTypeList = new List<PlayerStatType>();

        m_playerStatTypeList.Add(PlayerStatType.Years);
        m_playerStatTypeList.Add(PlayerStatType.FireRange);
        m_playerStatTypeList.Add(PlayerStatType.FireRate);
        m_playerStatTypeList.Add(PlayerStatType.IncomeMultiplier);
    }


    public PlayerStatType GetRandomUpgrade()
    {
        if (m_playerStatTypeList.Count == 0 || m_playerStatTypeList == null)
            ResetPool();

        int randomIndex = Random.Range(0, m_playerStatTypeList.Count);

        PlayerStatType randomUpgradeType = m_playerStatTypeList[randomIndex];

        m_playerStatTypeList.RemoveAt(randomIndex);

        return randomUpgradeType;
    }

}
