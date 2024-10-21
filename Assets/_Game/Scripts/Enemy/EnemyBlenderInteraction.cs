using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlenderInteraction : MonoBehaviour
{
    public static System.Action<GameObject> OnEnterBlender;

    [SerializeField]
    private GameObject m_controlledObject = null;

    [SerializeField]
    private ColliderObjectDetector m_blenderDetector = null;



    private void OnEnable()
    {
        m_blenderDetector.OnObjectDetected += OnBlenderDetected;
    }

    private void OnDisable()
    {
        m_blenderDetector.OnObjectDetected -= OnBlenderDetected;
    }


    private void OnBlenderDetected(GameObject blenderColliderObject)
    {
        OnEnterBlender?.Invoke(blenderColliderObject);

        Destroy(m_controlledObject);
    }


}
