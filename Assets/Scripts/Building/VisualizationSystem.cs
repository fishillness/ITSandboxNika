using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationSystem : MonoBehaviour
{
    
    [SerializeField] private SpriteRenderer m_MainSprite;
    [SerializeField] private Color m_GreenColor;
    [SerializeField] private Color m_RedColor;
    [SerializeField] private int m_DefaultSortingOrder;
    [SerializeField] private SpriteRenderer m_CellPrefab;
    [SerializeField] private float m_CellSize;
    [SerializeField] private List<SpriteRenderer> cells = new List<SpriteRenderer>();
    [SerializeField] private Transform m_CellContainer;
    
    public void ImpossibleToPlaceABuilding()
    {
        m_MainSprite.color = m_RedColor;
        m_MainSprite.sortingOrder = 1000;
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].gameObject.SetActive(true);
            cells[i].color = m_RedColor;
        }
    }
    public void PossibleToPlaceABuilding()
    {
        m_MainSprite.color = m_GreenColor;
        m_MainSprite.sortingOrder = 1000;
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].gameObject.SetActive(true);
            cells[i].color = m_GreenColor;
        }
    }
    public void BuildingIsLocated()
    {
        m_MainSprite.sortingOrder = m_DefaultSortingOrder;
        m_MainSprite.color = Color.white;
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].gameObject.SetActive(false);
        }
    }

    public void CellSpawn(Vector2 cellGridCount)
    {
        if (Application.isPlaying) return;

        cells.Clear();

        GameObject[] allObject = new GameObject[m_CellContainer.childCount];

        for (int i = 0; i < m_CellContainer.childCount; i++)
        {
            allObject[i] = m_CellContainer.GetChild(i).gameObject;
        }
        for (int i = 0; i < allObject.Length; i++)
        {
            DestroyImmediate(allObject[i]);
        }

        for (int i = 0; i < cellGridCount.x; i++)
        {
            for (int j = 0; j < cellGridCount.y; j++)
            {
                SpriteRenderer spriteRenderer = Instantiate(m_CellPrefab, m_CellContainer);
                spriteRenderer.transform.localPosition = new Vector3(i * m_CellSize,0, j * m_CellSize);
                spriteRenderer.transform.localScale = Vector3.one * m_CellSize;
                cells.Add(spriteRenderer);
            }
        }
    }
    
}
