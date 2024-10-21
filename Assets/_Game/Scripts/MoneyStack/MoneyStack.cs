using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStack : MonoBehaviour
{
    public static System.Action<float> OnMoneyStackCollected;

    [SerializeField]
    private Transform m_controlledTransform = null;

    [SerializeField]
    private ColliderObjectDetector m_playerDetector = null;

    [SerializeField]
    private Block m_block = null;

    [SerializeField]
    private float m_moneyStackValue = 1f;

    [SerializeField]
    private float m_gravityValue = -9.81f;




    private void OnEnable()
    {
        if (m_block != null)
            m_block.OnBlockDestroyed += OnBlockDestroyed;
    }

    private void OnDisable()
    {
        if (m_block != null)
            m_block.OnBlockDestroyed -= OnBlockDestroyed;

        m_playerDetector.OnObjectDetected -= OnPlayerDetected;
    }

    private void Start()
    {
        if (m_block == null)
            EnableInteractionWithPlayer();
    }

    private void OnBlockDestroyed()
    {
        m_playerDetector.OnObjectDetected += OnPlayerDetected;

        StartCoroutine(DropMoneyStackCoroutine());
    }

    private void EnableInteractionWithPlayer()
    {
        m_playerDetector.OnObjectDetected += OnPlayerDetected;
    }

    private IEnumerator DropMoneyStackCoroutine()
    {
        float velocity = 0f;
        Vector3 desiredPosition = m_controlledTransform.position;

        while (m_controlledTransform.position.y > 0)
        {
            yield return new WaitForEndOfFrame();

            velocity += m_gravityValue * Time.deltaTime;
            desiredPosition.y += velocity * Time.deltaTime;

            m_controlledTransform.position = desiredPosition;
        }

        desiredPosition.y = 0f;

        m_controlledTransform.position = desiredPosition;
    }


    private void OnPlayerDetected(GameObject playerColliderObject)
    {
        OnMoneyStackCollected?.Invoke(m_moneyStackValue);
        //todo pop fx
        Destroy(m_controlledTransform.gameObject);
    }

}
