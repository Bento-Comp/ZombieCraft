using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentProceduralLevelElements : MonoBehaviour
{
    public System.Action OnSegmentGenerated;
    public static System.Action<GameObject> OnProceduralLevelElementGenerated;

    [SerializeField]
    private bool m_isEndLevel = false;

    [Header("The 2 following list must be in order.")]
    [SerializeField]
    private List<GameObject> m_levelElementsList = null;

    [SerializeField]
    private List<Transform> m_levelElementsParenList = null;

    [SerializeField]
    private float m_xPositionStep = 2;

    [SerializeField]
    private float m_zPositionStep = 15;

    [SerializeField]
    private float m_chanceToSkipARow = 0.25f;


    private List<List<Vector3>> m_availablePositionList = new List<List<Vector3>>();
    private GameObject m_previousGameElementCreated;


    private void OnEnable()
    {
        ProceduralLevel.OnBroadcastProceduralLevel += GenerateProceduralLevelElements;
    }

    private void OnDisable()
    {
        ProceduralLevel.OnBroadcastProceduralLevel -= GenerateProceduralLevelElements;
    }


    private void GenerateProceduralLevelElements(float segmentLength)
    {
        if (m_isEndLevel)
            return;

        CalculateAvailablePositions(segmentLength);

        GenerateLevelElements();
    }


    private void CalculateAvailablePositions(float segmentLength)
    {
        Vector3 position = Vector3.zero;

        int zStepCount = (int)(segmentLength / m_zPositionStep) - 2;


        for (int i = 0; i < zStepCount; i++)
        {
            List<Vector3> rowPositionList = new List<Vector3>();

            position.z = m_zPositionStep * (i + 2);

            position.x = -m_xPositionStep;
            rowPositionList.Add(position);

            position.x = 0;
            rowPositionList.Add(position);

            position.x = m_xPositionStep;
            rowPositionList.Add(position);

            m_availablePositionList.Add(rowPositionList);
        }
    }


    private void GenerateLevelElements()
    {
        for (int i = 0; i < m_availablePositionList.Count; i++)
        {
            if (Random.Range(0f, 1f) < m_chanceToSkipARow)
                continue;

            int elementsToSpawnInARow = Random.Range(1, 3);

            if (elementsToSpawnInARow == 1)
            {
                CreateLevelElement(i, 1);
            }
            else if (elementsToSpawnInARow == 2)
            {
                CreateLevelElement(i, 0);
                CreateLevelElement(i, 2);
            }
        }


        OnSegmentGenerated?.Invoke();
    }

    private void CreateLevelElement(int rowCount, int xPositionIndex)
    {
        int randomIndex = Random.Range(0, m_levelElementsList.Count);

        GameObject levelElementToInstatiate = m_levelElementsList[randomIndex];
        Vector3 position = m_availablePositionList[rowCount][xPositionIndex];


        if (m_previousGameElementCreated == m_levelElementsList[randomIndex])
        {
            randomIndex = (randomIndex + 1) % m_levelElementsList.Count;
            levelElementToInstatiate = m_levelElementsList[randomIndex];
        }

        m_previousGameElementCreated = levelElementToInstatiate;


        GameObject levelElement = Instantiate(levelElementToInstatiate, m_levelElementsParenList[randomIndex]);

        levelElement.transform.localPosition = position;

        OnProceduralLevelElementGenerated?.Invoke(levelElement);
    }
}
