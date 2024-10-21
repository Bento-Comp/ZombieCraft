using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevel : MonoBehaviour
{
    public static System.Action<float> OnBroadcastProceduralLevel;

    [Header("Segments")]
    [SerializeField]
    private int m_numberOfSegments = 1;

    [SerializeField]
    private bool m_isNumberOfSegmentsRandom = false;

    [SerializeField]
    private int m_minNumberOfSegments = 1;

    [SerializeField]
    private int m_maxNumberOfSegments = 3;

    [SerializeField]
    private GameObject m_segmentPrefab = null;

    [SerializeField]
    private GameObject m_endLevelSegmentPrefab = null;

    [SerializeField]
    private float m_segmentLength = 100f;


    private List<LevelSegment> m_levelSegmentList = new List<LevelSegment>();


    private void Start()
    {
        CreateProceduralLevel();
    }



    private void CreateProceduralLevel()
    {
        int numberOfSegments = m_numberOfSegments;

        if (m_isNumberOfSegmentsRandom)
            numberOfSegments = Random.Range(m_minNumberOfSegments, m_maxNumberOfSegments);


        float segmentPositionStep = m_segmentLength;


        // LEVEL
        for (int i = 0; i < numberOfSegments; i++)
        {
            Vector3 spawnPosition = Vector3.zero;
            spawnPosition.z = segmentPositionStep * i;

            GameObject instantiatedSegment = Instantiate(m_segmentPrefab, spawnPosition, Quaternion.identity, transform);

            LevelSegment currentLevelSegment = instantiatedSegment.GetComponent<LevelSegment>();


            if(currentLevelSegment == null)
            {
                Debug.LogError("Couldn't get Level Segment component!", gameObject);
            }
            else
            {
                m_levelSegmentList.Add(currentLevelSegment);
                currentLevelSegment.SetLevelSegmentLength(m_segmentLength);
            }

        }


        // END OF LEVEL
        Vector3 endLevelSegmentSpawnPosition = Vector3.zero;
        endLevelSegmentSpawnPosition.z = numberOfSegments * segmentPositionStep;
        GameObject instantiatedEndLevelSegment = Instantiate(m_endLevelSegmentPrefab, endLevelSegmentSpawnPosition, Quaternion.identity, transform);

        LevelSegment currentEndLevelSegment = instantiatedEndLevelSegment.GetComponent<LevelSegment>();

        if (currentEndLevelSegment == null)
        {
            Debug.LogError("Couldn't get Level Segment component!", gameObject);
        }
        else
        {
            m_levelSegmentList.Add(currentEndLevelSegment);
            currentEndLevelSegment.SetLevelSegmentLength(m_segmentLength);
        }


        OnBroadcastProceduralLevel?.Invoke(m_segmentLength);
    }

}
