using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationSystem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_MainSprite;
    [SerializeField] private Color m_GreenColor;
    [SerializeField] private Color m_RedColor;
    [SerializeField] private SpriteRenderer m_CellPrefab;
    [SerializeField] private float m_CellSize;
    [SerializeField] private List<SpriteRenderer> cells = new List<SpriteRenderer>();
    
    public void ImpossibleToPlaceABuilding()
    {
        m_MainSprite.color = m_RedColor;
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].gameObject.SetActive(true);
            cells[i].color = m_RedColor;
        }
    }
    public void PossibleToPlaceABuilding()
    {
        m_MainSprite.color = m_GreenColor;
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].gameObject.SetActive(true);
            cells[i].color = m_GreenColor;
        }
    }
    public void BuildingIsLocated()
    {
        m_MainSprite.color = Color.white;
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].gameObject.SetActive(false);
        }
    }

    public void CellSpawnSpawn(Vector2 cellGridCount)
    {
        for (int i = 0; i < cellGridCount.x; i++)
        {
            for (int j = 0; j < cellGridCount.y; j++)
            {
                SpriteRenderer spriteRenderer = Instantiate(m_CellPrefab, transform);
                spriteRenderer.transform.localPosition = new Vector3(i * m_CellSize,0, j * m_CellSize);
                spriteRenderer.transform.localScale = Vector3.one * m_CellSize;
                cells.Add(spriteRenderer);
            }
        }
    }
    
}
