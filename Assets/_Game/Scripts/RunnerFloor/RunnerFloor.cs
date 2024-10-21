using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RunnerFloor : MonoBehaviour
{
    [SerializeField]
    private Transform m_scaleController = null;

    [SerializeField]
    private MeshRenderer m_conveyorBeltMeshRenderer = null;

    // 3.333 is a ratio. If the model has a scale of 50, it means the tiling needed for the texture is 15.
    [SerializeField]
    private float m_yTilingScaleOffsetRatio = 4f;


    private Vector2 m_textureTiling = Vector2.zero;



    private void Update()
    {
        if (m_scaleController == null || m_conveyorBeltMeshRenderer == null)
            return;

        UpdateConveyorBeltScale();
    }


    private void UpdateConveyorBeltScale()
    {
        m_textureTiling.x = 1f;
        m_textureTiling.y = m_scaleController.localScale.z / m_yTilingScaleOffsetRatio;

        m_conveyorBeltMeshRenderer.sharedMaterials[0].mainTextureScale = m_textureTiling;
    }
}
