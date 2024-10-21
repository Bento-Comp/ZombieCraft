using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static System.Action<float> OnMoneyUpdate;
    public static System.Action<float> OnGainMoney;

    [SerializeField]
    private string m_moneyID = "Money";

    // To change the implementation
    [SerializeField]
    private PlayerStat_IncomeMultiplier m_incomeMultiplier = null;

    private float m_currentMoney;

    public static MoneyManager Instance;

    public float CurrentMoney { get => m_currentMoney; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        MoneyStack.OnMoneyStackCollected += OnMoneyStackCollected;
    }

    private void OnDisable()
    {
        MoneyStack.OnMoneyStackCollected -= OnMoneyStackCollected;
    }



    private void Start()
    {
        LoadMoney();
    }


    private void OnMoneyStackCollected(float amount)
    {
        m_currentMoney += Mathf.Floor(amount * m_incomeMultiplier.StatValue);
        OnGainMoney?.Invoke(Mathf.Floor(amount * m_incomeMultiplier.StatValue));

        OnMoneyUpdate?.Invoke(m_currentMoney);
        SaveMoney();
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetFloat(m_moneyID, m_currentMoney);
    }

    private void LoadMoney()
    {
        if (PlayerPrefs.HasKey(m_moneyID))
            m_currentMoney = PlayerPrefs.GetFloat(m_moneyID);
        else
            SaveMoney();

        OnMoneyUpdate?.Invoke(m_currentMoney);
    }


    public bool HasEnoughMoney(float cost)
    {
        return cost <= m_currentMoney;
    }

    public void SpendMoney(float amount)
    {
        if (HasEnoughMoney(amount) == false)
            return;

        m_currentMoney -= amount;
        SaveMoney();
        OnMoneyUpdate?.Invoke(m_currentMoney);
    }
}
