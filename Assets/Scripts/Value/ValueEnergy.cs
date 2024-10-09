using System;
using System.Collections;
using UnityEngine;

public class ValueEnergy : MonoBehaviour
{
    public const string Filename = "EnergyTime12";

    [SerializeField] private int energy_Recovering;
    [SerializeField] private int time_Restoration;
    [SerializeField] private ValueManager valueManager;

    [SerializeField]
    public class EnergyTimeData
    {
        public DateTime time;
    }

    private IEnumerator Start()
    {
        if ((DateTime.Now - LoadStoreData()).TotalMinutes >= time_Restoration)
        {
            StartCoroutine(DateTimeToUnixTimestamp());
        }
        else
        {
            yield return new WaitForSeconds((float)(DateTime.Now - LoadStoreData()).TotalSeconds);
            StartCoroutine(AddEnergy());
        }
    }

    private IEnumerator DateTimeToUnixTimestamp()
    {
        var oldTime = LoadStoreData();
        var dif = (DateTime.Now - oldTime).TotalMinutes;

            var n = (int)dif / energy_Recovering;
            valueManager.AddResources(0, 0, 0, 0, energy: energy_Recovering * n);
            SaveStoreData();
            yield return new WaitForSeconds(time_Restoration * 60);
            StartCoroutine(AddEnergy());
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

    private IEnumerator AddEnergy()
    {
        while (true)
        {
            valueManager.AddResources(0, 0, 0, 0, energy: energy_Recovering);
            SaveStoreData();
            yield return new WaitForSeconds(time_Restoration * 60);
        }
    }
}
