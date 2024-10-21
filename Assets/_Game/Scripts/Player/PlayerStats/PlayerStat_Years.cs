using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat_Years : PlayerStat
{
    // float : years value; float : gains
    public static System.Action<float, float> OnUpdateYearsStat;
    public static System.Action<float> OnSendInitialYear;

    [Header("Years parameters")]

    // Example with m_yearsToFireRateRatio = 0.5f => bonus for each threshold passed

    [SerializeField]
    private float m_fireRateBonus = 0.5f;

    [SerializeField]
    private float m_fireRangeBonus = 0.5f;

    // Example with m_computationGranularity = 15f => bonus is given every 15 unit threshold

    [SerializeField]
    private float m_computationGranularity = 15f;

    [SerializeField]
    private float m_yearsLossWhenHittingObstacle = 1f;

    public float FireRateByYears { get => ComputeYears_FireRate(); }
    public float FireRangeByYears { get => ComputeYears_FireRange(); }


    protected override void OnEnable()
    {
        base.OnEnable();
        Gate.OnPlayerEnterGate_Global += OnPlayerEnterGate;
        ObstaclePlayerDetector.OnPlayerHitObstacle += OnPlayerHitObstacle;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Gate.OnPlayerEnterGate_Global -= OnPlayerEnterGate;
        ObstaclePlayerDetector.OnPlayerHitObstacle -= OnPlayerHitObstacle;
    }

    protected override void Start()
    {
        base.Start();

        OnSendInitialYear?.Invoke(GetStatValueAtLevel(0));
        OnUpdateYearsStat?.Invoke(m_statTotalValue, 0);
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    private void OnPlayerHitObstacle()
    {
        IncreaseCollectedUpgradeLevel(-m_yearsLossWhenHittingObstacle);
    }

    private void OnPlayerEnterGate(float bonusAmount, int gateNumber)
    {
        IncreaseCollectedUpgradeLevel(bonusAmount);
    }

    protected override void IncreaseCollectedUpgradeLevel(float bonusAmount)
    {
        base.IncreaseCollectedUpgradeLevel(bonusAmount);

        OnUpdateYearsStat?.Invoke(m_statTotalValue, bonusAmount);
    }

    private float ComputeYears_FireRate()
    {
        return (int)(m_statTotalLevel / m_computationGranularity) * m_fireRateBonus;
    }

    private float ComputeYears_FireRange()
    {
        return (int)(m_statTotalLevel / m_computationGranularity) * m_fireRangeBonus;
    }

}
