using UnityEngine;

[CreateAssetMenu]
public class BuildingInfo : ScriptableObject
{
    public Building Building => m_Building;
    public Sprite StoreCellImage => m_StoreCellImage;
    public int NeededBoards => m_NeededBoards;
    public int NeededCoins => m_NeededCoins;
    public int NeededNails => m_NeededNails;
    public int NeededBricks => m_NeededBricks;

    [SerializeField] private Building m_Building;
    [SerializeField] private Sprite m_StoreCellImage;

    [SerializeField] private int m_NeededCoins;
    [SerializeField] private int m_NeededBoards;
    [SerializeField] private int m_NeededBricks;
    [SerializeField] private int m_NeededNails;
}   
