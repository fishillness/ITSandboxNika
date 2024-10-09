using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ValueManager;

public class ValueEnergy : MonoBehaviour
{
    public const string Filename = "EnergyTime";

    [SerializeField] private int energy_Recovering;
    [SerializeField] private int time_Restoration;
    [SerializeField] private ValueManager valueManager;

    [SerializeField]
    public class EnergyTimeData
    {
        public DateTime time;
    }

    private void Awake()
    {
        DateTimeToUnixTimestamp();
    }

    private IEnumerable Start()
    {
        if((DateTime.Now - LoadStoreData()).TotalMinutes < time_Restoration)
            yield return new WaitForSeconds((float)(DateTime.Now - LoadStoreData()).TotalSeconds);
    }

    void Update()
    {

    }

    private void DateTimeToUnixTimestamp()
    {
        var oldTime = LoadStoreData();
        var dif = (DateTime.Now - oldTime).TotalMinutes;
        if (dif >= time_Restoration)
        {
            var n = (int)dif / energy_Recovering;
            valueManager.AddResources(0, 0, 0, 0, energy: energy_Recovering * n);
            SaveStoreData();
        }
    }

    private void SaveStoreData()
    {
        EnergyTimeData storeData = new EnergyTimeData();

        storeData.time = DateTime.Now;

        Saver<EnergyTimeData>.Save(Filename, storeData);
    }

    private DateTime LoadStoreData()
    {

        EnergyTimeData storeData = new EnergyTimeData();

        if (Saver<EnergyTimeData>.TryLoad(Filename, ref storeData) == false)
        {
            return DateTime.Now;
        }
        else
        {
            return storeData.time;
        }
    }

    private IEnumerable AddEnergy()
    {
        while (true)
        {
            valueManager.AddResources(0, 0, 0, 0, energy: energy_Recovering);
            yield return new WaitForSeconds(time_Restoration * 60);
        }
    }
}
