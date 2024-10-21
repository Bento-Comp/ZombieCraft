using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LevelSegment : MonoBehaviour
{
    [SerializeField]
    private float m_segmentLength = 50f;

    [SerializeField]
    private List<Transform> m_scaleControllerList = null;

    [SerializeField]
    private Transform m_endOfLevelObject = null;



    private void Update()
    {
        if (m_scaleControllerList == null || m_endOfLevelObject == null)
            return;


        UpdateScalesAndPositions();

    }

    public void SetLevelSegmentLength(float length)
    {
        m_segmentLength = length;
    }

    private void UpdateScalesAndPositions()
    {
        for (int i = 0; i < m_scaleControllerList.Count; i++)
        {
            Vector3 scaleBuffer = m_scaleControllerList[i].localScale;
            scaleBuffer.z = m_segmentLength;
            m_scaleControllerList[i].localScale = scaleBuffer;
        }


        m_endOfLevelObject.position = transform.position + Vector3.forward * m_segmentLength;

    }

}
