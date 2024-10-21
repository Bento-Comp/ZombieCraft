using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBlender : MonoBehaviour
{
    public System.Action<float> OnSendProgression;


    [Header("Zombie in blender tracking")]
    [SerializeField]
    private Transform m_enemiesParentTransform = null;

    [SerializeField]
    private SegmentProceduralLevelElements m_segmentProceduralLevelElements = null;

    [SerializeField, Tooltip("Used when a zombie reach the blender")]
    private GameObject m_blenderColliderObject = null;

    [SerializeField]
    private ParticleSystem m_fx = null;

    private int m_totalEnemyCount;
    private int m_currentEnemyInBlenderCount;


    private void OnEnable()
    {
        EnemyBlenderInteraction.OnEnterBlender += OnEnemyEnterBlender;
        m_segmentProceduralLevelElements.OnSegmentGenerated += OnSegmentGenerated;
    }

    private void OnDisable()
    {
        EnemyBlenderInteraction.OnEnterBlender -= OnEnemyEnterBlender;
        m_segmentProceduralLevelElements.OnSegmentGenerated -= OnSegmentGenerated;
    }


    private void Start()
    {
        Initialize();
    }


    private void Initialize()
    {
        m_totalEnemyCount = m_enemiesParentTransform.childCount;

        if(m_totalEnemyCount == 0)
        {
            //HIDE GATES
        }

        m_fx.Stop();
    }

    private void OnSegmentGenerated()
    {
        m_totalEnemyCount = m_enemiesParentTransform.childCount;
    }

    private void OnEnemyEnterBlender(GameObject blenderColliderObject)
    {
        if (m_totalEnemyCount== 0)
            return;

        if (blenderColliderObject != m_blenderColliderObject)
            return;

        m_fx.Play();

        m_currentEnemyInBlenderCount++;

        float progression = Mathf.Clamp01((float)m_currentEnemyInBlenderCount / m_totalEnemyCount);
        
        OnSendProgression?.Invoke(progression);
    }

}
