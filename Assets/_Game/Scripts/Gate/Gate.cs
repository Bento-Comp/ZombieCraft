using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public System.Action<float, float, float> OnSendGatesBonus;
    //int : gate number
    public System.Action<int> OnEnableGate;
    // float : bonus; int : gate number
    public static System.Action<float, int> OnPlayerEnterGate_Global;
    public System.Action<float, int> OnPlayerEnterGate;


    [SerializeField]
    private ZombieBlender m_zombieBlender = null;

    [Header("Gate 1")]
    [SerializeField]
    private ColliderObjectDetector m_gate1PlayerDetector = null;

    [SerializeField]
    private Collider m_gate1PlayerDetectorCollider = null;

    [SerializeField]
    private float m_gate1YearsBonus = 3f;


    [Header("Gate 2")]
    [SerializeField]
    private ColliderObjectDetector m_gate2PlayerDetector = null;

    [SerializeField]
    private Collider m_gate2PlayerDetectorCollider = null;

    [SerializeField]
    private float m_gate2YearsBonus = 7f;


    [Header("Gate 3")]
    [SerializeField]
    private ColliderObjectDetector m_gate3PlayerDetector = null;

    [SerializeField]
    private Collider m_gate3PlayerDetectorCollider = null;

    [SerializeField]
    private float m_gate3YearsBonus = 12f;



    private void OnEnable()
    {
        m_zombieBlender.OnSendProgression += OnEnemyEnterBlender;

        m_gate1PlayerDetector.OnObjectDetected += OnGate1DetectPlayer;
        m_gate2PlayerDetector.OnObjectDetected += OnGate2DetectPlayer;
        m_gate3PlayerDetector.OnObjectDetected += OnGate3DetectPlayer;
    }

    private void OnDisable()
    {
        m_zombieBlender.OnSendProgression -= OnEnemyEnterBlender;

        m_gate1PlayerDetector.OnObjectDetected -= OnGate1DetectPlayer;
        m_gate2PlayerDetector.OnObjectDetected -= OnGate2DetectPlayer;
        m_gate3PlayerDetector.OnObjectDetected -= OnGate3DetectPlayer;
    }

    private void Start()
    {
        TogglePlayerDetection(false);
        OnSendGatesBonus?.Invoke(m_gate1YearsBonus, m_gate2YearsBonus, m_gate3YearsBonus);
    }


    private void TogglePlayerDetection(bool state)
    {
        m_gate1PlayerDetectorCollider.enabled = state;
        m_gate2PlayerDetectorCollider.enabled = state;
        m_gate3PlayerDetectorCollider.enabled = state;
    }

    private void OnEnemyEnterBlender(float progression)
    {
        float delta = 0.01f;

        if (progression > 1f / 3f - delta)
        {
            m_gate1PlayerDetectorCollider.enabled = true;
            OnEnableGate?.Invoke(1);
        }

        if (progression > 2f / 3f - delta)
        {
            m_gate2PlayerDetectorCollider.enabled = true;
            OnEnableGate?.Invoke(2);
        }

        if (progression >= 1f - delta)
        {
            m_gate3PlayerDetectorCollider.enabled = true;
            OnEnableGate?.Invoke(3);
        }
    }


    private void OnGate1DetectPlayer(GameObject playerCollider)
    {
        TogglePlayerDetection(false);
        OnPlayerEnterGate_Global?.Invoke(m_gate1YearsBonus, 1);
        OnPlayerEnterGate?.Invoke(m_gate1YearsBonus, 1);
    }

    private void OnGate2DetectPlayer(GameObject playerCollider)
    {
        TogglePlayerDetection(false);
        OnPlayerEnterGate_Global?.Invoke(m_gate2YearsBonus, 2);
        OnPlayerEnterGate?.Invoke(m_gate2YearsBonus, 2);
    }

    private void OnGate3DetectPlayer(GameObject playerCollider)
    {
        TogglePlayerDetection(false);
        OnPlayerEnterGate_Global?.Invoke(m_gate3YearsBonus, 3);
        OnPlayerEnterGate?.Invoke(m_gate3YearsBonus, 3);
    }

}
