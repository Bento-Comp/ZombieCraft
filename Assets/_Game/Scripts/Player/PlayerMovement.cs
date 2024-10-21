using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerBump m_playerBump = null;

    [SerializeField]
    private Transform m_controlledTransform = null;

    [SerializeField]
    private float m_pixelToWorldRatio = 0.01f;

    [SerializeField]
    private float m_zMovementSpeed = 5f;

    [SerializeField]
    private float m_xMovementSmoothTime = 0.3f;

    [SerializeField]
    private float m_levelLeftXBorder = -4f;

    [SerializeField]
    private float m_levelRightXBorder = 4f;

    private Vector3 m_desiredPosition;
    private Vector3 m_playerStartPosition;
    private float m_desiredXPosition;
    private float m_xVelocity;
    private bool m_canMove;
    private bool m_isMovementEnabled;


    private void OnEnable()
    {
        ControllerComputer.OnSendTapStartPosition += OnSendTapStartPosition;
        ControllerComputer.OnSendDirectionMagnitude += OnSendDirection;

        m_playerBump.OnBumpStart += OnBumpStart;
        m_playerBump.OnBumpStop += OnBumpStop;

        PlayerState.OnPlayerEnabled += OnPlayerEnabled;
        PlayerState.OnPlayerDisabled += OnPlayerDisabled;
    }

    private void OnDisable()
    {
        ControllerComputer.OnSendTapStartPosition -= OnSendTapStartPosition;
        ControllerComputer.OnSendDirectionMagnitude -= OnSendDirection;

        m_playerBump.OnBumpStart -= OnBumpStart;
        m_playerBump.OnBumpStop -= OnBumpStop;

        PlayerState.OnPlayerEnabled -= OnPlayerEnabled;
        PlayerState.OnPlayerDisabled -= OnPlayerDisabled;
    }

    private void Update()
    {
        if (m_canMove && m_isMovementEnabled)
            Move();
    }


    private void OnPlayerEnabled()
    {
        Initialize();
    }

    private void OnPlayerDisabled()
    {
        m_isMovementEnabled = false;
    }

    private void Initialize()
    {
        m_canMove = true;
        m_isMovementEnabled = true;
    }

    private void OnBumpStart()
    {
        m_canMove = false;
    }

    private void OnBumpStop()
    {
        m_canMove = true;
    }

    private void Move()
    {
        m_desiredPosition.x = Mathf.SmoothDamp(m_controlledTransform.position.x, m_desiredXPosition, ref m_xVelocity, m_xMovementSmoothTime);
        m_desiredPosition.y = 0;
        m_desiredPosition.z = m_controlledTransform.position.z + m_zMovementSpeed * Time.deltaTime;

        m_desiredPosition.x = Mathf.Clamp(m_desiredPosition.x, m_levelLeftXBorder, m_levelRightXBorder);

        m_controlledTransform.position = m_desiredPosition;
    }

    private void OnSendTapStartPosition(Vector3 tapStartPositionInPixel)
    {
        m_playerStartPosition = m_controlledTransform.position;
    }

    private void OnSendDirection(Vector3 directionMagnitudeInPixel)
    {
        m_desiredXPosition = m_playerStartPosition.x + directionMagnitudeInPixel.x * m_pixelToWorldRatio;
    }

}
