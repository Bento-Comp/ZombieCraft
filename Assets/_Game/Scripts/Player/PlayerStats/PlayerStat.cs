using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerStat : MonoBehaviour
{
    public static System.Action<UpgradeStatUI, PlayerStat> OnSendStatInfo;

    [SerializeField]
    private PlayerStatType m_statType;

    [SerializeField]
    private string m_statID = "";

    [SerializeField]
    private AnimationCurve m_statCurve = null;

    [SerializeField]
    private AnimationCurve m_costCurve = null;

    [Header("DEBUG SECTION")]
    [SerializeField]
    private bool m_isDebugEnabled = false;

    [SerializeField]
    private float m_statValueDEBUG;

    [SerializeField]
    private float m_statLevelDEBUG;

    [SerializeField]
    private KeyCode m_upgradePurchasedLevelUpKeyCode;

    [SerializeField]
    private KeyCode m_upgradeCollectedLevelUpKeyCode;

    public PlayerStatType StatType { get => m_statType; }
    public float StatCost { get => GetCurrentUpgradeCost(); }
    public float StatValue { get => m_statTotalValue; }
    public float StatLevel { get => m_statTotalLevel; }
    protected float m_statTotalValue { get => ComputeStatValue(); }
    protected float m_statTotalLevel { get => ComputeStatLevel(); }
    public float StatPurchasedLevel { get => m_statPurchasedLevel; }
    public float StatCollectedLevel { get => m_statCollectedLevel; }

    protected float m_statPurchasedLevel;
    protected float m_statCollectedLevel;


    protected virtual void OnEnable()
    {
        Portal.OnPlayerCrossPortal += OnPlayerCrossPortal;
        GamePhaseManager.OnTitleScreen += ResetCollectedUpgradeLevel;

        UpgradeStatUI.OnAskStatInfo += OnAskStatInfo;
        UpgradeStatUI.OnPurchaseUpgrade += OnPurchaseUpgrade;
    }

    protected virtual void OnDisable()
    {
        Portal.OnPlayerCrossPortal -= OnPlayerCrossPortal;
        GamePhaseManager.OnTitleScreen -= ResetCollectedUpgradeLevel;

        UpgradeStatUI.OnAskStatInfo -= OnAskStatInfo;
        UpgradeStatUI.OnPurchaseUpgrade -= OnPurchaseUpgrade;
    }


    private void Awake()
    {
        Initialize();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if (m_isDebugEnabled == false)
            return;

        if (Input.GetKeyDown(m_upgradePurchasedLevelUpKeyCode))
        {
            OnUpgradePurchased();
        }

        if (Input.GetKeyDown(m_upgradeCollectedLevelUpKeyCode))
        {
            IncreaseCollectedUpgradeLevel(1);
        }

        m_statLevelDEBUG = m_statTotalLevel;
        m_statValueDEBUG = m_statTotalValue;
    }

    protected virtual void Initialize()
    {
        LoadPurchasedUpgradeLevel();
    }


    private void OnAskStatInfo(UpgradeStatUI upgradeStatUI, PlayerStatType statType)
    {
        if (m_statType != statType)
            return;

        OnSendStatInfo?.Invoke(upgradeStatUI, this);
    }

    protected float GetStatValueAtLevel(int level)
    {
        return m_statCurve.Evaluate(level);
    }

    // STAT VALUE MANAGEMENT =================

    protected virtual float ComputeStatValue()
    {
        return GetStatFromCurve();
    }

    protected float GetStatFromCurve()
    {
        return m_statCurve.Evaluate(m_statTotalLevel);
    }


    // LEVEL MANAGEMENT =================

    private float ComputeStatLevel()
    {
        return m_statPurchasedLevel + m_statCollectedLevel;
    }


    // PURCHASED UPGRADES BEHAVIOR
    private void SavePurchasedUpgradeLevel()
    {
        PlayerPrefs.SetFloat(m_statID, m_statPurchasedLevel);
    }

    private void LoadPurchasedUpgradeLevel()
    {
        if (PlayerPrefs.HasKey(m_statID))
            m_statPurchasedLevel = PlayerPrefs.GetFloat(m_statID);
        else
            SavePurchasedUpgradeLevel();
    }

    private float GetCurrentUpgradeCost()
    {
        return Mathf.Floor(m_costCurve.Evaluate(m_statPurchasedLevel));
    }

    private void OnPurchaseUpgrade(PlayerStatType upgradeType)
    {
        if (upgradeType != m_statType)
            return;

        if (MoneyManager.Instance.HasEnoughMoney(StatCost) == false)
        {
            return;
        }

        MoneyManager.Instance.SpendMoney(StatCost);

        OnUpgradePurchased();
    }

    protected void OnUpgradePurchased()
    {
        m_statPurchasedLevel++;

        SavePurchasedUpgradeLevel();
    }


    // COLLECTED UPGRADES BEHAVIOR
    private void ResetCollectedUpgradeLevel()
    {
        m_statCollectedLevel = 0;
    }

    protected virtual void IncreaseCollectedUpgradeLevel(float levelToIncrease)
    {
        if (m_statType == PlayerStatType.Years)
            Debug.Log("gained " + levelToIncrease);

        m_statCollectedLevel += levelToIncrease;

        if (m_statCollectedLevel < 0)
        {
            m_statCollectedLevel = 0f;
        }
    }

    private void OnPlayerCrossPortal(PlayerStatType playerStatType, float levelValue)
    {
        if (playerStatType != m_statType)
            return;

        IncreaseCollectedUpgradeLevel(levelValue);
    }
}
