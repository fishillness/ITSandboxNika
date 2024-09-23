using System;
using UnityEngine;

public class ValueManager : MonoBehaviour
{
    public const string Filename = "Value";

    [Serializable]
    public class ValueData
    {
        public int CoinsCount;
        public int BoardsCount;
        public int BricksCount;
        public int NailsCount;

        public int Advancement;
        public int Cosiness;
        public int Health;
        public int Joy;
    }

    public int CoinsCount => m_Coins.CurrentValue;
    public int BoardsCount => m_Boards.CurrentValue;
    public int BricksCount => m_Bricks.CurrentValue;
    public int NailsCount => m_Nails.CurrentValue;

    [SerializeField] private Resource m_Coins;
    [SerializeField] private Resource m_Boards;
    [SerializeField] private Resource m_Bricks;
    [SerializeField] private Resource m_Nails;

    [SerializeField] private ShelterCharacteristic m_Advancement;
    [SerializeField] private ShelterCharacteristic m_Cosiness;
    [SerializeField] private ShelterCharacteristic m_Health;
    [SerializeField] private ShelterCharacteristic m_Joy;

    private void Awake()
    {
        m_Advancement.SetMaxValue();
        m_Cosiness.SetMaxValue();
        m_Health.SetMaxValue();
        m_Joy.SetMaxValue();

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
    public void DeleteShelterCharacteristics(int advancement, int cosiness, int health, int joy)
    {
        m_Advancement.DeleteValue(advancement);
        m_Cosiness.DeleteValue(cosiness);
        m_Health.DeleteValue(health);
        m_Joy.DeleteValue(joy);

        SaveStoreData();
    }

    public void AddShelterCharacteristics(int advancement, int cosiness, int health, int joy)
    {
        m_Advancement.AddValue(advancement);
        m_Cosiness.AddValue(cosiness);
        m_Health.AddValue(health);
        m_Joy.AddValue(joy);

        SaveStoreData();
    }
    private void SaveStoreData()
    {
        ValueData storeData = new ValueData();

        storeData.CoinsCount = m_Coins.CurrentValue;
        storeData.BoardsCount = m_Boards.CurrentValue;
        storeData.BricksCount = m_Bricks.CurrentValue;
        storeData.NailsCount = m_Nails.CurrentValue;

        storeData.Advancement = m_Advancement.CurrentValue;
        storeData.Cosiness = m_Cosiness.CurrentValue;
        storeData.Health = m_Health.CurrentValue;
        storeData.Joy = m_Joy.CurrentValue;

        Saver<ValueData>.Save(Filename, storeData);
    }

    private void LoadStoreData()
    {        

        ValueData storeData = new ValueData();

        if (Saver<ValueData>.TryLoad(Filename, ref storeData) == false)
        {
            m_Coins.StartValue();
            m_Boards.StartValue();
            m_Bricks.StartValue();
            m_Nails.StartValue();
        }
        else
        {
            m_Coins.SetCurrentValue(storeData.CoinsCount);
            m_Boards.SetCurrentValue(storeData.BoardsCount);
            m_Bricks.SetCurrentValue(storeData.BricksCount);
            m_Nails.SetCurrentValue(storeData.NailsCount);

            m_Advancement.SetCurrentValue(storeData.Advancement);
            m_Cosiness.SetCurrentValue(storeData.Cosiness);
            m_Health.SetCurrentValue(storeData.Health);
            m_Joy.SetCurrentValue(storeData.Joy);
        }        
    }
}
