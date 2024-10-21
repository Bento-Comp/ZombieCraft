using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public System.Action<float> OnSendCurrentValueForVisual;
    public System.Action<float> OnSendImpactValueForVisual;
    public System.Action<PlayerStatType> OnSendTitleForVisual;
    public static System.Action<PlayerStatType, float> OnPlayerCrossPortal;
    public static System.Action OnBulletHitPortal;

    [SerializeField]
    private GameObject m_controlledObject = null;

    [SerializeField]
    private Animator m_animator = null;

    [SerializeField]
    private PlayerStatType m_bonusType;

    [SerializeField]
    private Collider m_bulletDetectorCollider = null;

    [SerializeField]
    private ColliderObjectDetector m_playerDetector = null;

    [SerializeField]
    private float m_initialValue = 0f;

    [SerializeField]
    private float m_impactOnValue = 1f;


    private float m_currenValue;


    private void OnEnable()
    {
        BulletController.OnHitPortal += OnBulletDetected;
        m_playerDetector.OnObjectDetected += OnPlayerDetected;
        SegmentProceduralLevelElements.OnProceduralLevelElementGenerated += OnProceduralLevelElementGenerated;
    }

    private void OnDisable()
    {
        BulletController.OnHitPortal -= OnBulletDetected;
        m_playerDetector.OnObjectDetected -= OnPlayerDetected;
        SegmentProceduralLevelElements.OnProceduralLevelElementGenerated -= OnProceduralLevelElementGenerated;
    }


    private void Start()
    {
        m_currenValue = m_initialValue;
        SendPortalInfos();
    }

    private void SendPortalInfos()
    {
        OnSendCurrentValueForVisual?.Invoke(m_currenValue);
        OnSendImpactValueForVisual?.Invoke(m_impactOnValue);
        OnSendTitleForVisual?.Invoke(m_bonusType);
    }

    private void OnBulletDetected(GameObject bulletDetectorCollider)
    {
        if (m_bulletDetectorCollider.gameObject != bulletDetectorCollider)
            return;

        m_animator.SetTrigger("Bump");

        m_currenValue += m_impactOnValue;
        OnSendCurrentValueForVisual?.Invoke(m_currenValue);

        OnBulletHitPortal?.Invoke();
    }

    private void OnPlayerDetected(GameObject playerColliderObject)
    {
        OnPlayerCrossPortal?.Invoke(m_bonusType, m_currenValue);
        BulletController.OnHitPortal -= OnBulletDetected;
        Destroy(m_controlledObject);
    }


    private void OnProceduralLevelElementGenerated(GameObject levelElement)
    {
        if (m_controlledObject == levelElement)
        {
            SetRandomParameters();
        }
    }

    private void SetRandomParameters()
    {
        PlayerStatType randomStat = RandomEnum.RandomEnumValue<PlayerStatType>();
        m_bonusType = randomStat;

        m_currenValue = Random.Range(0, 11);

        /*
        if (m_currenValue / 5 > 5)
        {
            m_impactOnValue = Random.Range(1, 3);
        }
        else if (m_currenValue > 2)
        {
            m_impactOnValue = Random.Range(2, 5);
        }
        */
        m_impactOnValue = 1f;


        if (Random.Range(0f, 1f) > 0.5f)
        {
            m_currenValue = Mathf.Abs(m_currenValue) * -1f;
        }

        SendPortalInfos();
    }
}
