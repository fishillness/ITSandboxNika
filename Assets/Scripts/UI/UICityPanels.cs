using UnityEngine;

public class UICityPanels : MonoBehaviour
{
    [SerializeField] private NotepadController notepadController;
    [SerializeField] private UIMissionController missionController;
    [SerializeField] private UISettingController settingController;
    [SerializeField] private UIMatch3LevelInfoPanel match3LevelInfoPanel;
    [SerializeField] private UIBottomPanelButtons bottomPanelButtons;

    public NotepadController NotepadController => notepadController;
    public UIMissionController MissionController => missionController;
    public UISettingController SettingController => settingController;
    public UIMatch3LevelInfoPanel Match3LevelInfoPanel => match3LevelInfoPanel;
    public UIBottomPanelButtons BottomPanelButtons => bottomPanelButtons;
}
