using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    private Transform m_scaleController = null;

    [SerializeField]
    private MeshRenderer m_conveyorBeltMeshRenderer = null;

    [SerializeField]
    private float m_textureScrollingSpeed = 3f;

    // 3.333 is a ratio. If the model has a scale of 50, it means the tiling needed for the texture is 15.
    [SerializeField]
    private float m_yTilingScaleOffsetRatio = 3.333f;


    private Vector2 m_textureOffset = Vector2.zero;
    private Vector2 m_textureTiling = Vector2.zero;



    private void Update()
    {
        if (m_scaleController == null || m_conveyorBeltMeshRenderer == null)
            return;

        UpdateConveyorBeltScale();
        ManageConveyorBeltAnimation();
    }


    private void UpdateConveyorBeltScale()
    {
        m_textureTiling.x = 1f;
        m_textureTiling.y = m_scaleController.localScale.z / m_yTilingScaleOffsetRatio;

        if (m_conveyorBeltMeshRenderer.sharedMaterials.Length > 0 && m_conveyorBeltMeshRenderer != null && m_conveyorBeltMeshRenderer.sharedMaterials != null)
            m_conveyorBeltMeshRenderer.sharedMaterials[0].mainTextureScale = m_textureTiling;
    }


    private void ManageConveyorBeltAnimation()
    {
        m_textureOffset.y += m_textureScrollingSpeed * Time.deltaTime;

        m_conveyorBeltMeshRenderer.sharedMaterials[0].mainTextureOffset = m_textureOffset;
    }


}
