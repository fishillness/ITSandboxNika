using System;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public const string Filename = "Store";

    [Serializable]
    public class StoreData
    {
        public int CoinsCount;
        public int BoardsCount;
        public int BricksCount;
        public int NailsCount;
    }

    public int CoinsCount => m_Coins.CurrentValue;
    public int BoardsCount => m_Boards.CurrentValue;
    public int BricksCount => m_Bricks.CurrentValue;
    public int NailsCount => m_Nails.CurrentValue;

    [SerializeField] private Resource m_Coins;
    [SerializeField] private Resource m_Boards;
    [SerializeField] private Resource m_Bricks;
    [SerializeField] private Resource m_Nails;

    private void Awake()
    {
        LoadStoreData();
    }

    public void DeleteResources(int coins, int boards, int bricks, int nails)
    {
        m_Coins.DeleteValue(coins);
        m_Boards.DeleteValue(boards);
        m_Bricks.DeleteValue(bricks);
        m_Nails.DeleteValue(nails);

        SaveStoreData();
    }

    public void AddResources(int coins, int boards, int bricks, int nails)
    {
        m_Coins.AddValue(coins);
        m_Boards.AddValue(boards);
        m_Bricks.AddValue(bricks);
        m_Nails.AddValue(nails);

        SaveStoreData();
    }

    private void SaveStoreData()
    {
        StoreData storeData = new StoreData();

        storeData.CoinsCount = m_Coins.CurrentValue;
        storeData.BoardsCount = m_Boards.CurrentValue;
        storeData.BricksCount = m_Bricks.CurrentValue;
        storeData.NailsCount = m_Nails.CurrentValue;

        Saver<StoreData>.Save(Filename, storeData);
    }

    private void LoadStoreData()
    {
        StoreData storeData = new StoreData();

        if (Saver<StoreData>.TryLoad(Filename, ref storeData) == false)
        {
            m_Coins.MaxValue();
            m_Boards.MaxValue();
            m_Bricks.MaxValue();
            m_Nails.MaxValue();
        }
        else
        {
            m_Coins.SetCurrentValue(storeData.CoinsCount);
            m_Boards.SetCurrentValue(storeData.BoardsCount);
            m_Bricks.SetCurrentValue(storeData.BricksCount);
            m_Nails.SetCurrentValue(storeData.NailsCount);
        }        
    }
}
