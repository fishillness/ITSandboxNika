using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2 m_Size;
    [SerializeField] private float m_CellSize;
    [SerializeField] private SpriteRenderer m_Cell;
    [SerializeField] private int m_BuildIndex;

    private void Start()
    {
        m_Cell.size = m_Size;
        m_Cell.transform.localScale = Vector3.one * m_CellSize;
    }
}
