using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Constraint_Position : MonoBehaviour
{
    [SerializeField]
    private Transform m_controlledTransform = null;

    [SerializeField]
    private Transform m_targetTransform = null;

    [SerializeField]
    private bool m_followX = false;

    [SerializeField]
    private bool m_followY = false;

    [SerializeField]
    private bool m_followZ = false;

    public bool IsConstraintEnabled = false;

    private Vector3 m_desiredTargetPosition;


    private void LateUpdate()
    {
        if (IsConstraintEnabled == false)
            return;

        if (m_controlledTransform != null && m_targetTransform != null)
        {
            m_desiredTargetPosition.x = m_followX ? m_targetTransform.position.x : m_controlledTransform.position.x;

            m_desiredTargetPosition.y = m_followY ? m_targetTransform.position.y : m_controlledTransform.position.y;

            m_desiredTargetPosition.z = m_followZ ? m_targetTransform.position.z : m_controlledTransform.position.z;

            m_controlledTransform.position = m_desiredTargetPosition;
        }
    }

}
