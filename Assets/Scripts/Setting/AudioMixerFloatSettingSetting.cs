using System;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioMixerFloatSettingSetting : Setting
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string nameParameter;

    // -80..0
    [SerializeField] private float minRealValue;
    [SerializeField] private float maxRealValue;
    // 0..100
    [SerializeField] private float virtualStep;
    [SerializeField] private float minVirtualValue;
    [SerializeField] private float maxVirtualValue;

    private float currentValue = 0;

    public override bool IsMinValue { get => currentValue == minRealValue; }
    public override bool IsMaxValue { get => currentValue == maxRealValue; }

    public float VirtualStep => virtualStep;
    public float MinVirtualValue => minVirtualValue;
    public float MaxVirtualValue => maxVirtualValue;

    public override void SetNextValue()
    {
        AddValue(Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override void SetPreviousValue()
    {
        AddValue(-Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override string GetStringValue()
    {
        return Mathf.Lerp(minVirtualValue, maxVirtualValue,
            (currentValue - minRealValue) / (maxRealValue - minRealValue)).ToString();
    }

    public override object GetValue()
    {
        return currentValue;
    }

    public override void Apply()
    {
        audioMixer.SetFloat(nameParameter, currentValue);
        Save();
    }

    public void SetValue(float value)
    {
        currentValue = minRealValue - (minRealValue * value) / 100;
    }

    private void AddValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
    }

    public void SetMaxValue()
    {
        currentValue = maxRealValue;
    }

    public void SetMinValue()
    {
        currentValue = minRealValue;
    }

    public override void Load()
    {
        // currentValue = Saves.LoadFloat(title, 0); 

        if (nameParameter == "SFX Volume")
        {
            SFXSettingsData uploadedSFXData = new SFXSettingsData();

            if (Saver<SFXSettingsData>.TryLoad(SaverFilenames.SFXSettingsFilaname, ref uploadedSFXData) == true)
            {
                currentValue = uploadedSFXData.SFXValue;
            }
            else
            {
                SetMaxValue(); ;
            }
        }
        else if (nameParameter == "Music Volume")
        {
            MusicSettingsData uploadedMusicData = new MusicSettingsData();

            if (Saver<MusicSettingsData>.TryLoad(SaverFilenames.MusicSettingsFilaname, ref uploadedMusicData) == true)
            {
                currentValue = uploadedMusicData.MusicValue;
            }
            else
            {
                SetMaxValue();
            }
        }
    }

    private void Save()
    {
        //Saves.SaveFloat(title, currentValue); 

        if (nameParameter == "SFX Volume")
        {
            SFXSettingsData SFXData = new SFXSettingsData();

            SFXData.SFXValue = currentValue;

            Saver<SFXSettingsData>.Save(SaverFilenames.SFXSettingsFilaname, SFXData);
        }
        else if (nameParameter == "Music Volume")
        {
            MusicSettingsData musicData = new MusicSettingsData();

            musicData.MusicValue = currentValue;

            Saver<MusicSettingsData>.Save(SaverFilenames.MusicSettingsFilaname, musicData);
        }
    }

    //Temporary
    [Serializable]
    public class SFXSettingsData
    {
        public float SFXValue;
    }
    [Serializable]
    public class MusicSettingsData
    { 
        public float MusicValue;
    }
}
