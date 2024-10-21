using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat_FireRate : PlayerStat
{
    [SerializeField]
    private PlayerStat_Years m_playerStatYears = null;


    protected override float ComputeStatValue()
    {
        return GetStatFromCurve() + m_playerStatYears.FireRateByYears;
    }

}
