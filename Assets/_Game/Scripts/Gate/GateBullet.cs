using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBullet : MonoBehaviour
{
    [SerializeField]
    private Transform m_controlledTransform = null;

    [SerializeField]
    private float m_xMovementSmoothTime = 0.5f;


    private Vector3 m_destination;
    private Vector3 m_desiredPosition;
    private float m_xVelocity;


    private void Update()
    {
        m_desiredPosition = m_controlledTransform.position;
        m_desiredPosition.x = Mathf.SmoothDamp(m_controlledTransform.position.x, m_destination.x, ref m_xVelocity, m_xMovementSmoothTime);
        m_controlledTransform.position = m_desiredPosition;
    }


    public void SetDestination(Vector3 destination)
    {
        m_destination = destination;
    }






}
