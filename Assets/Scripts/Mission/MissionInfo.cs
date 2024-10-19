using UnityEngine;

[CreateAssetMenu]
public class MissionInfo : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private MissionType type;
    [SerializeField] private string text;

    [Header("if type GetResource")]
    [SerializeField] private ValueType valueType;
    [SerializeField] private int value;

    [Header("if type GetGetBuilding")]
    [SerializeField] private bool anyBuilding = true;
    [Header("if current building")]
    [SerializeField] private BuildingInfo buildingInfo;

    public int Id => id;
    public MissionType Type => type;
    public string Text => text;
    public ValueType ValueType => valueType;
    public int NeedGetValue => value;
    public bool AnyBuilding => anyBuilding;
    public BuildingInfo BuildingInfo => buildingInfo;

}
