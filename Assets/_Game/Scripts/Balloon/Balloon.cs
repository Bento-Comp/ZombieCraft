using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField]
    private Transform m_balloonTransform = null;

    [SerializeField]
    private float m_startScale = 10f;

    [SerializeField]
    private float m_targetScale = 60f;

    [SerializeField]
    private float m_scaleUpTime = 0.5f;


    private float m_scaleUpSpeed { get => (m_targetScale - m_startScale) / m_scaleUpTime; }


    private void OnEnable()
    {
        StartCoroutine(ScaleUpCoroutine());
    }

    private IEnumerator ScaleUpCoroutine()
    {
        m_balloonTransform.localScale = Vector3.one * m_startScale;

        while (m_balloonTransform.localScale.x < m_targetScale)
        {
            yield return new WaitForEndOfFrame();

            m_balloonTransform.localScale += Vector3.one * m_scaleUpSpeed * Time.deltaTime;
        }

        m_balloonTransform.localScale = Vector3.one * m_targetScale;
    }

}
