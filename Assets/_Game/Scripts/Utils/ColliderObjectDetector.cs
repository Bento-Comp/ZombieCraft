using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderObjectDetector : MonoBehaviour
{
    public Action<GameObject> OnObjectDetected;
    public Action<GameObject> OnObjectNotDetectedAnymore;

    [SerializeField]
    private string m_objectTagToDetect = "";

    [SerializeField]
    private bool m_isDebugEnabled = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_objectTagToDetect))
        {
            if (m_isDebugEnabled)
                Debug.Log("Hit");

            OnObjectDetected?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(m_objectTagToDetect))
        {
            OnObjectNotDetectedAnymore?.Invoke(other.gameObject);
        }
    }
}
