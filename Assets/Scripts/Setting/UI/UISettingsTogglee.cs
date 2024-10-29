using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsTogglee : MonoBehaviour
{
    [SerializeField] private AudioMixerFloatSettingSetting audioSetting;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Toggle toggle;

    private void Start()
    {
        toggle.onValueChanged.AddListener(OnSwitch);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }

    private void OnSwitch(bool on)
    {
        if (toggle.isOn)
        {
            audioSetting.SetMaxValue();
            audioSetting.Apply();
        }
        else
        {
            audioSetting.SetMinValue();
            audioSetting.Apply();
        }
    }

    public void ApplyProperty()
    {
        if (audioSetting == null) return;

        audioSetting.Load();
        audioSetting.Apply();

        titleText.text = audioSetting.Title;

        if (audioSetting.IsMinValue && toggle.isOn)
        {
            toggle.isOn = false;
        }

        if (audioSetting.IsMaxValue && !toggle.isOn)
        {
            toggle.isOn = true;
        }
    }
}
