using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateVisual : MonoBehaviour
{
    [Header("Visual parameters")]
    [SerializeField]
    private GameObject m_bulletModelPrefab = null;

    [SerializeField]
    private Transform m_gateBulletParent = null;

    [SerializeField]
    private float m_bulletScale = 0.7f;

    [SerializeField]
    private int m_maxBulletsModel = 12;

    [SerializeField]
    private ZombieBlender m_zombieBlender = null;


    private List<GateBullet> m_bulletsModelList = new List<GateBullet>();
    private GameObject m_instantiatedBulletBuffer;
    private GateBullet m_gateBulletBuffer;
    private Vector3 m_gateBulletPositionBuffer;



    private void OnEnable()
    {
        m_zombieBlender.OnSendProgression += OnEnemyEnterBlender;

        Gate.OnPlayerEnterGate_Global += OnPlayerEnterGate;
    }

    private void OnDisable()
    {
        m_zombieBlender.OnSendProgression -= OnEnemyEnterBlender;

        Gate.OnPlayerEnterGate_Global -= OnPlayerEnterGate;
    }


    private void OnPlayerEnterGate(float bonus, int gateNumber)
    {
        m_bulletsModelList.Reverse();

        int startIndex = (gateNumber - 1) * m_maxBulletsModel / 3;
        startIndex = Mathf.Clamp(startIndex, 0, m_bulletsModelList.Count);

        int endIndex = startIndex + m_maxBulletsModel / 3;
        endIndex = Mathf.Clamp(endIndex, 0, m_bulletsModelList.Count);


        for (int i = startIndex; i < endIndex; i++)
        {
            if(i < m_bulletsModelList.Count)
            {
                m_bulletsModelList[i].gameObject.SetActive(false);
            }
        }
    }


    private void OnEnemyEnterBlender(float progression)
    {
        float visualBulletsProgression = (float)m_bulletsModelList.Count / m_maxBulletsModel;

        while (visualBulletsProgression < progression)
        {
            AddBullet();

            visualBulletsProgression = (float)m_bulletsModelList.Count / m_maxBulletsModel;
        }
    }


    private void AddBullet()
    {
        if (m_bulletsModelList.Count >= m_maxBulletsModel)
            return;

        m_instantiatedBulletBuffer = Instantiate(m_bulletModelPrefab, m_gateBulletParent.position, Quaternion.identity, m_gateBulletParent);
        m_gateBulletBuffer = m_instantiatedBulletBuffer.GetComponent<GateBullet>();

        if (m_gateBulletBuffer == null)
            Debug.LogError("Could not get GateBullet component");

        m_bulletsModelList.Add(m_gateBulletBuffer);

        for (int i = 0; i < m_bulletsModelList.Count; i++)
        {
            int reverseIndex = m_bulletsModelList.Count - 1 - i;

            m_gateBulletPositionBuffer = m_gateBulletParent.position + Vector3.right * m_bulletScale * reverseIndex;

            m_bulletsModelList[i].SetDestination(m_gateBulletPositionBuffer);
        }
    }



}
