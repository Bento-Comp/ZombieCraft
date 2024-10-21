using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBump : MonoBehaviour
{
    public System.Action OnBumpStart;
    public System.Action OnBumpStop;


    [SerializeField]
    private Transform m_controlledTransform = null;

    [SerializeField]
    private float m_bumpDistance = 3f;

    [SerializeField]
    private float m_bumpDuration = 1f;

    [SerializeField]
    private float m_zBumpSmoothTime = 0.3f;


    private Coroutine m_bumpCoroutine;
    private float m_bumpTimer;
    private float m_zVelocity;


    private void OnEnable()
    {
        ObstaclePlayerDetector.OnPlayerHitObstacle += OnPlayerHitObstacle;
        ObstaclePlayerDetector.OnPlayerHitDeadlyObstacle += OnPlayerHitObstacle;
    }

    private void OnDisable()
    {
        ObstaclePlayerDetector.OnPlayerHitObstacle -= OnPlayerHitObstacle;
        ObstaclePlayerDetector.OnPlayerHitDeadlyObstacle -= OnPlayerHitObstacle;
    }



    private void OnPlayerHitObstacle()
    {
        if (m_bumpCoroutine != null)
            StopCoroutine(m_bumpCoroutine);

        m_bumpCoroutine = StartCoroutine(BumpCoroutine());
    }

    private IEnumerator BumpCoroutine()
    {
        m_bumpTimer = 0f;

        float zDesiredPosition = m_controlledTransform.position.z - m_bumpDistance;
        Vector3 desiredPosition = m_controlledTransform.position;

        OnBumpStart?.Invoke();

        while (m_bumpTimer < m_bumpDuration)
        {
            m_bumpTimer += Time.deltaTime;

            desiredPosition.z = Mathf.SmoothDamp(m_controlledTransform.position.z, zDesiredPosition, ref m_zVelocity, m_zBumpSmoothTime);

            m_controlledTransform.position = desiredPosition;

            yield return new WaitForEndOfFrame();
        }

        OnBumpStop?.Invoke();
    }

}
