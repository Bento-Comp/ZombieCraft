using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(2)]
public class UpgradeStatUI : MonoBehaviour
{
    public static System.Action<UpgradeStatUI, PlayerStatType> OnAskStatInfo;
    public static System.Action<PlayerStatType> OnPurchaseUpgrade;

    [SerializeField]
    private PlayerStatType m_upgradeType;

    [SerializeField]
    private bool m_isUpgradeRandom = false;



    [SerializeField]
    private TMP_Text m_statTitleText = null;

    [SerializeField]
    private TMP_Text m_statLevelText = null;

    [SerializeField]
    private TMP_Text m_priceText = null;


    [SerializeField]
    private Image m_statImage = null;

    [SerializeField]
    private Sprite m_fireRangeSprite = null;

    [SerializeField]
    private Sprite m_fireRateSprite = null;

    [SerializeField]
    private Sprite m_yearsSprite = null;

    [SerializeField]
    private Sprite m_incomeMultiplierSprite = null;


    [SerializeField]
    private GameObject m_buttonWhenUpgradeIsPurchasable = null;

    [SerializeField]
    private GameObject m_buttonWhenUpgradeIsAvailableByAd = null;


    [SerializeField]
    private Button m_purchaseUpgradeButton = null;


    private PlayerStatType m_randomUpgradeType;


    private void OnEnable()
    {
        PlayerStat.OnSendStatInfo += OnSendStatInfo;
        MoneyManager.OnMoneyUpdate += OnMoneyUpdate;

        if (m_isUpgradeRandom)
        {
            DetermineRandomStat();
        }

        UpdateUI();
    }

    private void OnDisable()
    {
        PlayerStat.OnSendStatInfo -= OnSendStatInfo;
        MoneyManager.OnMoneyUpdate -= OnMoneyUpdate;
    }

    private void OnMoneyUpdate(float currentMoney)
    {
        UpdateUI();
    }

    private void DetermineRandomStat()
    {
        //m_randomUpgradeType = (PlayerStatType)Random.Range(0, System.Enum.GetValues(typeof(PlayerStatType)).Length);

        m_randomUpgradeType = RandomUpgradesPoolManager.Instance.GetRandomUpgrade();
    }

    private void UpdateUI()
    {
        if (m_isUpgradeRandom)
            OnAskStatInfo?.Invoke(this, m_randomUpgradeType);
        else
            OnAskStatInfo?.Invoke(this, m_upgradeType);
    }


    private void OnSendStatInfo(UpgradeStatUI upgradeStatUI, PlayerStat stat)
    {
        if (upgradeStatUI != this)
            return;

        switch (stat.StatType)
        {
            case PlayerStatType.Years:
                m_statTitleText.text = "Years";
                m_statImage.sprite = m_yearsSprite;
                break;
            case PlayerStatType.FireRange:
                m_statTitleText.text = "Fire Range";
                m_statImage.sprite = m_fireRangeSprite;
                break;
            case PlayerStatType.FireRate:
                m_statTitleText.text = "Fire Rate";
                m_statImage.sprite = m_fireRateSprite;
                break;
            case PlayerStatType.IncomeMultiplier:
                m_statTitleText.text = "Income";
                m_statImage.sprite = m_incomeMultiplierSprite;
                break;
            default:
                break;
        }

        m_statLevelText.text = "Level " + stat.StatPurchasedLevel.ToString("F0");


        bool isUpgradePurchasable = MoneyManager.Instance.HasEnoughMoney(stat.StatCost);

        // todo : check if an ad is available, discuss with team
        if (isUpgradePurchasable)
        {
            m_priceText.text = FormatGameText.FormatValue(stat.StatCost);
            m_priceText.color = Color.white;
            m_purchaseUpgradeButton.interactable = true;

            m_buttonWhenUpgradeIsPurchasable.SetActive(true);
            m_buttonWhenUpgradeIsAvailableByAd.SetActive(false);
        }
        else
        {
            m_priceText.text = FormatGameText.FormatValue(stat.StatCost);
            m_priceText.color = Color.red;
            m_purchaseUpgradeButton.interactable = false;

            m_buttonWhenUpgradeIsPurchasable.SetActive(true);
            m_buttonWhenUpgradeIsAvailableByAd.SetActive(false);
        }
    }


    // todo : modify with the ads
    public void PurchaseUpgrade()
    {
        if (m_isUpgradeRandom)
            OnPurchaseUpgrade?.Invoke(m_randomUpgradeType);
        else
            OnPurchaseUpgrade?.Invoke(m_upgradeType);

        UpdateUI();
    }



}
