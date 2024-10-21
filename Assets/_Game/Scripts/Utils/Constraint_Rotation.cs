using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Constraint_Rotation : MonoBehaviour
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


    private Vector3 m_desiredTargetRotation;


    private void LateUpdate()
    {
        if (m_controlledTransform != null && m_targetTransform != null)
        {
            m_desiredTargetRotation.x = m_followX ? m_targetTransform.rotation.eulerAngles.x : m_controlledTransform.rotation.eulerAngles.x;

            m_desiredTargetRotation.y = m_followY ? m_targetTransform.rotation.eulerAngles.y : m_controlledTransform.rotation.eulerAngles.y;

            m_desiredTargetRotation.z = m_followZ ? m_targetTransform.rotation.eulerAngles.z : m_controlledTransform.rotation.eulerAngles.z;

            m_controlledTransform.rotation= Quaternion.Euler(m_desiredTargetRotation);
        }
    }
}
