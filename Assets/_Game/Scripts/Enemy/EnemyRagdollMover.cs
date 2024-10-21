using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdollMover : MonoBehaviour
{
    [SerializeField]
    private Rigidbody m_parentRigidBody = null;

    [SerializeField]
    private Rigidbody m_pelvisRigidBody = null;

    [SerializeField]
    private Constraint_Position m_positionConstraintRagdollRoot = null;

    [SerializeField]
    private EnemyRagdollController m_enemyRagdollController = null;

    [SerializeField]
    private GameObject m_balloon = null;

    [SerializeField]
    private float m_delayBeforeMovingRagdoll = 2f;

    // param
    [SerializeField]
    private float m_xPositionForDeadEnemyRagdoll = -6.5f;

    [SerializeField]
    private float m_xSpeed = 2f;

    [SerializeField]
    private float m_xMovementSmoothTime = 0.3f;

    // param
    [SerializeField]
    private float m_yPositionForDeadEnemyRagdoll = 4f;

    [SerializeField]
    private float m_ySpeed = 5f;

    [SerializeField]
    private float m_yMovementSmoothTime = 0.3f;

    // param
    [SerializeField]
    private float m_zSpeed = 5f;


    private Vector3 m_desiredPosition;
    private float m_xVelocity;
    private float m_yVelocity;
    private bool m_canMoveRagdoll;


    private void OnEnable()
    {
        m_enemyRagdollController.OnRagdollEnabled += OnRagdollEnabled;
    }

    private void OnDisable()
    {
        m_enemyRagdollController.OnRagdollEnabled -= OnRagdollEnabled;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        Move();

    }

    private void Initialize()
    {
        m_positionConstraintRagdollRoot.IsConstraintEnabled = false;
        m_canMoveRagdoll = false;
    }

    private void OnRagdollEnabled()
    {
        StartCoroutine(StartMovingRagdollCoroutine());
    }

    private IEnumerator StartMovingRagdollCoroutine()
    {
        yield return new WaitForSeconds(m_delayBeforeMovingRagdoll);
        m_canMoveRagdoll = true;
        m_parentRigidBody.isKinematic = true;
        m_pelvisRigidBody.isKinematic = true;
        m_parentRigidBody.position = m_pelvisRigidBody.position;

        m_balloon.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        m_positionConstraintRagdollRoot.IsConstraintEnabled = true;
    }


    private void Move()
    {
        if (m_canMoveRagdoll == false)
            return;

        if (m_parentRigidBody.position.y < m_yPositionForDeadEnemyRagdoll)
        {
            m_desiredPosition.x = Mathf.SmoothDamp(m_parentRigidBody.position.x, m_xPositionForDeadEnemyRagdoll, ref m_xVelocity, m_xMovementSmoothTime);
        }

        if (m_parentRigidBody.position.x > m_xPositionForDeadEnemyRagdoll)
        {
            m_desiredPosition.y = Mathf.SmoothDamp(m_parentRigidBody.position.y, m_yPositionForDeadEnemyRagdoll, ref m_yVelocity, m_yMovementSmoothTime);
        }

        m_desiredPosition.z = m_parentRigidBody.transform.position.z + m_zSpeed * Time.deltaTime;

        m_parentRigidBody.transform.position = m_desiredPosition;
    }


}
