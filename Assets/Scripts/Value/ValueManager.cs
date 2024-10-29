using System;
using UnityEngine;
using UnityEngine.Events;

public class ValueManager : MonoBehaviour
{
    [Serializable]
    public class ValueData
    {
        public int CoinsCount;
        public int BoardsCount;
        public int BricksCount;
        public int NailsCount;
        public int EnergyCount;

        public int Advancement;
        public int Cosiness;
        public int Health;
        public int Joy;
    }

    public int CoinsCount => m_Coins.CurrentValue;
    public int BoardsCount => m_Boards.CurrentValue;
    public int BricksCount => m_Bricks.CurrentValue;
    public int NailsCount => m_Nails.CurrentValue;
    public int EnergyCount => m_Energy.CurrentValue;

    public int Advancement => m_Advancement.CurrentValue;
    public int Cosiness => m_Cosiness.CurrentValue;
    public int Health => m_Health.CurrentValue;
    public int Joy => m_Joy.CurrentValue;

    public Resource Energy => m_Energy;

    [SerializeField] private Resource m_Coins;
    [SerializeField] private Resource m_Boards;
    [SerializeField] private Resource m_Bricks;
    [SerializeField] private Resource m_Nails;
    [SerializeField] private Resource m_Energy;

    [SerializeField] private ShelterCharacteristic m_Advancement;
    [SerializeField] private ShelterCharacteristic m_Cosiness;
    [SerializeField] private ShelterCharacteristic m_Health;
    [SerializeField] private ShelterCharacteristic m_Joy;

    private void Awake()
    {
        LoadStoreData();
    }

    public void DeleteResources(int coins, int boards, int bricks, int nails, int energy)
    {
        m_Coins.DeleteValue(coins);
        m_Boards.DeleteValue(boards);
        m_Bricks.DeleteValue(bricks);
        m_Nails.DeleteValue(nails);
        m_Energy.DeleteValue(energy);

        SaveStoreData();
    }

    public void AddResources(int coins, int boards, int bricks, int nails, int energy)
    {
        m_Coins.AddValue(coins);
        m_Boards.AddValue(boards);
        m_Bricks.AddValue(bricks);
        m_Nails.AddValue(nails);
        m_Energy.AddValue(energy);

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

    public void UpdateTime(string time)
    {
        m_Energy.UpdateTime(time);
    }

    public int SetMax()
    {
        return m_Energy.SetMaxValueResource();
    }

    public int SetCurrent() 
    {
        return m_Energy.SetCur();
    }

    public UnityEvent<int> GetEventOnValueChangeByType(ValueType type)
    {
        switch (type)
        {
            case ValueType.Coins:
                return m_Coins.OnValueChange;
            case ValueType.Boards:
                return m_Boards.OnValueChange;
            case ValueType.Bricks:
                return m_Bricks.OnValueChange;
            case ValueType.Nails:
                return m_Nails.OnValueChange;
            case ValueType.Energy:
                return m_Energy.OnValueChange;
            case ValueType.Advancement:
                return m_Advancement.OnValueChange;
            case ValueType.Cosiness:
                return m_Cosiness.OnValueChange;
            case ValueType.Health:
                return m_Health.OnValueChange;
            case ValueType.Joy:
                return m_Joy.OnValueChange;
        }

        return null;
    }
    public UnityEvent<int> GetEventOnValueAddByType(ValueType type)
    {
        switch (type)
        {
            case ValueType.Coins:
                return m_Coins.OnValueAdd;
            case ValueType.Boards:
                return m_Boards.OnValueAdd;
            case ValueType.Bricks:
                return m_Bricks.OnValueAdd;
            case ValueType.Nails:
                return m_Nails.OnValueAdd;
            case ValueType.Energy:
                return m_Energy.OnValueAdd;
            case ValueType.Advancement:
                return m_Advancement.OnValueAdd;
            case ValueType.Cosiness:
                return m_Cosiness.OnValueAdd;
            case ValueType.Health:
                return m_Health.OnValueAdd;
            case ValueType.Joy:
                return m_Joy.OnValueAdd;
        }

        return null;
    }

    public int GetValueByType(ValueType type)
    {
        switch (type)
        {
            case ValueType.Coins:
                return CoinsCount;
            case ValueType.Boards:
                return BoardsCount;
            case ValueType.Bricks:
                return BricksCount;
            case ValueType.Nails:
                return NailsCount;
            case ValueType.Energy:
                return EnergyCount;
            case ValueType.Advancement:
                return Advancement;
            case ValueType.Cosiness:
                return Cosiness;
            case ValueType.Health:
                return Health;
            case ValueType.Joy:
                return Joy;
        }

        return 0;
    }

    public int GetMaxValueByType(ValueType type)
    {
        switch (type)
        {
            case ValueType.Coins:
                return m_Coins.MaxValue;
            case ValueType.Boards:
                return m_Boards.MaxValue;
            case ValueType.Bricks:
                return m_Bricks.MaxValue;
            case ValueType.Nails:
                return m_Nails.MaxValue;
            case ValueType.Energy:
                return m_Energy.MaxValue;
            case ValueType.Advancement:
                return m_Advancement.MaxValue;
            case ValueType.Cosiness:
                return m_Cosiness.MaxValue;
            case ValueType.Health:
                return m_Health.MaxValue;
            case ValueType.Joy:
                return m_Joy.MaxValue;
        }

        return 0;
    }
    public int AddValueByType(ValueType type, int value)
    {
        switch (type)
        {
            case ValueType.Coins:
                AddResources(value, 0, 0, 0, 0);
                break;
            case ValueType.Boards:
                AddResources(0, value, 0, 0, 0);
                break;
            case ValueType.Bricks:
                AddResources(0, 0, value, 0, 0);
                break;
            case ValueType.Nails:
                AddResources(0, 0, 0, value, 0);
                break;
            case ValueType.Energy:
                AddResources(0, 0, 0, 0, value);
                break;
            case ValueType.Advancement:
                AddShelterCharacteristics(value, 0, 0, 0);
                break;
            case ValueType.Cosiness:
                AddShelterCharacteristics(0, value, 0, 0);
                break;
            case ValueType.Health:
                AddShelterCharacteristics(0, 0, value, 0);
                break;
            case ValueType.Joy:
                AddShelterCharacteristics(0, 0, 0, value);
                break;
        }
        return 0;
    }

    private void SaveStoreData()
    {
        ValueData storeData = new ValueData();

        storeData.CoinsCount = m_Coins.CurrentValue;
        storeData.BoardsCount = m_Boards.CurrentValue;
        storeData.BricksCount = m_Bricks.CurrentValue;
        storeData.NailsCount = m_Nails.CurrentValue;
        storeData.EnergyCount = m_Energy.CurrentValue;

        storeData.Advancement = m_Advancement.CurrentValue;
        storeData.Cosiness = m_Cosiness.CurrentValue;
        storeData.Health = m_Health.CurrentValue;
        storeData.Joy = m_Joy.CurrentValue;

        Saver<ValueData>.Save(SaverFilenames.ValueFilaname, storeData);
    }

    private void LoadStoreData()
    {        

        ValueData storeData = new ValueData();

        if (Saver<ValueData>.TryLoad(SaverFilenames.ValueFilaname, ref storeData) == false)
        {
            m_Coins.StartValue();
            m_Boards.StartValue();
            m_Bricks.StartValue();
            m_Nails.StartValue();
            m_Energy.StartValue();
        }
        else
        {
            m_Coins.SetCurrentValue(storeData.CoinsCount);
            m_Boards.SetCurrentValue(storeData.BoardsCount);
            m_Bricks.SetCurrentValue(storeData.BricksCount);
            m_Nails.SetCurrentValue(storeData.NailsCount);
            m_Energy.SetCurrentValue(storeData.EnergyCount);

            m_Advancement.SetCurrentValue(storeData.Advancement);
            m_Cosiness.SetCurrentValue(storeData.Cosiness);
            m_Health.SetCurrentValue(storeData.Health);
            m_Joy.SetCurrentValue(storeData.Joy);
        }        
    }
}
