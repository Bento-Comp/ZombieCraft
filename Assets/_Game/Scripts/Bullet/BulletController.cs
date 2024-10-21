using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static System.Action<GameObject> OnHitEnemy;
    public static System.Action<GameObject> OnHitPortal;
    public static System.Action<GameObject> OnHitBlock;

    [SerializeField]
    private GameObject m_controlledObject = null;

    [SerializeField]
    private ColliderObjectDetector m_enemyDetector = null;

    [SerializeField]
    private ColliderObjectDetector m_portalDetector = null;

    [SerializeField]
    private ColliderObjectDetector m_blockDetector = null;

    [SerializeField]
    private ColliderObjectDetector m_gateDetector = null;

    [SerializeField]
    private GameObject m_hitObstacleFxPrefab = null;


    private Vector3 m_startPosition;
    private float m_maxDistanceToTravel;


    private void OnEnable()
    {
        PlayerShoot.OnSendBulletInfos += OnSendBulletInfos;

        m_enemyDetector.OnObjectDetected += OnEnemyDetected;
        m_portalDetector.OnObjectDetected += OnPortalDetected;
        m_blockDetector.OnObjectDetected += OnBlockDetected;
        m_gateDetector.OnObjectDetected += OnGateDetected;
    }

    private void OnDisable()
    {
        PlayerShoot.OnSendBulletInfos -= OnSendBulletInfos;

        m_enemyDetector.OnObjectDetected -= OnEnemyDetected;
        m_portalDetector.OnObjectDetected -= OnPortalDetected;
        m_blockDetector.OnObjectDetected -= OnBlockDetected;
        m_gateDetector.OnObjectDetected -= OnGateDetected;
    }

    private void Update()
    {
        ManageDistanceTraveledCondition();
    }


    private void ManageDistanceTraveledCondition()
    {
        if ((m_controlledObject.transform.position.z - m_startPosition.z) > m_maxDistanceToTravel)
        {
            Destroy(m_controlledObject);
        }
    }

    private void OnEnemyDetected(GameObject enemyColliderObject)
    {
        OnHitEnemy?.Invoke(enemyColliderObject);

        CreateHitObstacleFx();

        Destroy(m_controlledObject);
    }


    private void OnPortalDetected(GameObject portalColliderObject)
    {
        OnHitPortal?.Invoke(portalColliderObject);

        CreateHitObstacleFx();

        Destroy(m_controlledObject);
    }

    private void OnBlockDetected(GameObject blockColliderObject)
    {
        OnHitBlock?.Invoke(blockColliderObject);

        CreateHitObstacleFx();

        Destroy(m_controlledObject);
    }

    private void OnSendBulletInfos(GameObject bulletReference, float shootRange)
    {
        if (m_controlledObject != bulletReference)
            return;

        m_maxDistanceToTravel = shootRange;

        m_startPosition = m_controlledObject.transform.position;
    }

    private void OnGateDetected(GameObject gateColliderObject)
    {
        CreateHitObstacleFx();
        Destroy(m_controlledObject);
    }

    private void CreateHitObstacleFx()
    {
        Instantiate(m_hitObstacleFxPrefab, m_controlledObject.transform.position, Quaternion.identity);
    }
}
