using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlayerDetector : MonoBehaviour
{
    public static System.Action OnPlayerHitObstacle;
    public static System.Action OnPlayerHitDeadlyObstacle;

    [SerializeField]
    private ColliderObjectDetector m_playerDetector = null;

    [SerializeField]
    private bool m_canDetectMoreThanOnce = false;

    [SerializeField]
    private bool m_isDeadlyForPlayer = false;


    private void OnEnable()
    {
        m_playerDetector.OnObjectDetected += OnPlayerDetected;
    }

    private void OnDisable()
    {
        m_playerDetector.OnObjectDetected -= OnPlayerDetected;
    }


    private void OnPlayerDetected(GameObject colliderObject)
    {
        if (m_canDetectMoreThanOnce == false)
            m_playerDetector.OnObjectDetected -= OnPlayerDetected;

        if (m_isDeadlyForPlayer)
            OnPlayerHitDeadlyObstacle?.Invoke();
        else
            OnPlayerHitObstacle?.Invoke();
    }
}
