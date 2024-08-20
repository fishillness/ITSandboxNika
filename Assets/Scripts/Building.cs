using UnityEngine;


public class Building : MonoBehaviour
{
    public Vector2 Size => m_Size;
    public int BuildingID => m_BuildingID;

    [SerializeField] private Vector2 m_Size;
    [SerializeField] private int m_BuildingID;
}
